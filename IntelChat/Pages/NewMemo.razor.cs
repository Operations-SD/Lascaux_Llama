using IntelChat.Models;
using IntelChat.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Security.Cryptography;
using ThreadingTask = System.Threading.Tasks.Task;

namespace IntelChat.Pages
{
	public partial class NewMemo

	{
       
        [Parameter]
		[SupplyParameterFromQuery]
		public int? pod { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
        
        public int? pid { get; set; }
		public string message = "";
		public string messageType = "";
		private List<Memo> memos = new List<Memo>();
		private List<Memo> currentSenderMemo = new List<Memo>(); // contains the memo of the current selecter sender
        [Inject]
        private NewStateService NewMemoService { get; set; } = default!; // Inject the service
        private bool showResponseMessage = false;
        private string selectedFileName;
        private string imageUrl;

        public int roleRecipient = 0;
		public string MessageTime = "";//time of the message that was sent

		private SqlDataReader? ExecuteStoredProcedure(string procedure, List<SqlParameter> parameters, bool reader = false)
		{
			var connection = new SqlConnection(_configuration.GetValue<string>("ConnectionStrings:DefaultConnection"));
			using var command = new SqlCommand(procedure, connection) { CommandType = CommandType.StoredProcedure };
			parameters.ForEach(parameter => command.Parameters.Add(parameter));
			connection.Open();
			if (reader) return command.ExecuteReader(CommandBehavior.CloseConnection);
			command.ExecuteNonQuery();
			return null;
		}

        private void CreateMemo()
        {
            if (messageType == "" || message == "") return;
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@PROC_action", "Create"),
                new SqlParameter("@memo_person_to",  roleRecipient),
                new SqlParameter("@memo_person_from", pid),
                new SqlParameter("@memo_date_time", DateTime.Now),
                new SqlParameter("@memo_priority", 0),
                new SqlParameter("@memo_pod", pod),
                new SqlParameter("@memo_nova", 0),
                new SqlParameter("@memo_channel", 0),
                new SqlParameter("@memo_type", messageType),
                new SqlParameter("@memo_status", "A"),
                new SqlParameter("@memo_message", message),
                new SqlParameter("@memo_image", (imageUrl != null) ? (object)imageUrl : DBNull.Value)
            };

            showResponseMessage = true;

            ExecuteStoredProcedure("dbo.[CRUD_Memo]", parameters);
            // Reset image-related fields after sending
            imageUrl = null;
            selectedFileName = null;
        }
        private void LoadMemos(string sp, string table)
		{
            showResponseMessage = false;
            var reader = Read(sp, table);
			if (reader == null) return;

			memos.Clear();
			while (reader.Read())
			{
				memos.Add(new Memo
				{
					MemoPersonTo = reader.GetInt32(0),
					MemoPersonFrom = reader.GetInt32(1),
					MemoDateTime = reader.GetDateTime(2),
					MemoPriority = reader.GetByte(4),
					GuideIdFk = reader.GetInt32(10),
					MemoNova = reader.GetInt32(12),
					MemoChannel = reader.GetInt32(8),
					MemoType = reader.GetString(5),
					MemoStatus = reader.GetString(6),
					MemoMessage = reader.GetString(7)
				});
			}
			reader.CloseAsync();
		}

		private int ReadPodRolePerson(string role)
		{
            if (role == "") return pid ?? 0;
            if (role == "self") return pid ?? 0;

			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@pod", pod),
				new SqlParameter("@role", role)
			};
			var reader = ExecuteStoredProcedure("dbo.[Read_POD_Role_Person]", parameters, true);
			if (reader == null) return 0;
			reader.Read();
			int recipientId = reader.GetInt32(0);
			reader.Close();
			return recipientId;
		}

		//This function takes in PID and Convert to the Roles
        private string ReadRoleFromPid(int? pid)
        {
            if (pid == null) return "Unknown"; // Default if pid is null

            List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@pid", pid)
			};

            var reader = ExecuteStoredProcedure("dbo.[Read_Person_Role]", parameters, true);

            if (reader == null || !reader.Read()) return "Unknown"; // Return default if no data

            string roleName = reader.GetString(0); // Fetch role name from first column
            reader.Close();

            return roleName;
        }


        private SqlDataReader? Read(string sp, string table)
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@pod", pod),
				new SqlParameter("@table", table)
			};
			return ExecuteStoredProcedure($"dbo.[{sp}]", parameters, true);
		}

		private void ChangeInbox(ChangeEventArgs e)
		{
			DateTime dt = DateTime.Parse(e.Value.ToString());
			Memo currentMemo = memos.Find(memo => memo.MemoDateTime.ToString() == dt.ToString());
			message = currentMemo.MemoMessage;
			messageType = currentMemo.MemoType;
			
		}

		



        private async void OnFileSelected(InputFileChangeEventArgs e)
        {
            try
            {
                var file = e.File;
                selectedFileName = file.Name;

                // Create a resized image file to reduce memory usage
                var imageFile = await file.RequestImageFileAsync(file.ContentType, 800, 600);

                // Use a memory stream to read the file
                using var stream = imageFile.OpenReadStream(maxAllowedSize: 5242880); // 5MB max
                using var ms = new MemoryStream();
                await stream.CopyToAsync(ms);

                // Convert to base64
                byte[] buffer = ms.ToArray();
                string fileType = file.ContentType;
                imageUrl = $"data:{fileType};base64,{Convert.ToBase64String(buffer)}";

                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing image: {ex.Message}");
                // Add error handling as needed
            }
        }






        //******************************* stored proceedure **************************************
        protected override void OnInitialized()
		{
			message = NewMemoService.message;
            LoadMemos("Read_Grid", "Memo");
		}
	}
}