using IntelChat.Models;
using IntelChat.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Security.Cryptography;
using ThreadingTask = System.Threading.Tasks.Task;

namespace IntelChat.Pages
{
	public partial class Inbox
	{

        
        [Parameter]
		[SupplyParameterFromQuery]
		public int? pod { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pid { get; set; }
        public bool modal = false;
        public string currentImage { get; set; } // Store current image data
        public string show = "";
        public int person1 = 0;
        public string message = "";
		public string messageType = "";
        private List<Memo> memos = new List<Memo>();
		private List<Memo> currentSenderMemo = new List<Memo>(); // contains the memo of the current selecter sender
        private int currentMemoIndex = -1;// for iteration of the current sender memos
       
        public string roleRecipient = "";
		public string MessageTime = "";//time of the message that was sent
        [Inject] NavigationManager NavigationManager { get; set; }

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
				new SqlParameter("@memo_person_to", ReadPodRolePerson(roleRecipient)),
				new SqlParameter("@memo_person_from", pid),
				new SqlParameter("@memo_date_time", DateTime.Now),
				new SqlParameter("@memo_priority", 0),
				new SqlParameter("@memo_pod", pod),
				new SqlParameter("@memo_nova", 0),
				new SqlParameter("@memo_channel", 0),
				new SqlParameter("@memo_type", messageType),
				new SqlParameter("@memo_status", "A"),
				new SqlParameter("@memo_message", message)
			};
			ExecuteStoredProcedure("dbo.[CRUD_Memo]", parameters);
		}
        private void LoadMemos(string sp, string table)
        {
            var reader = Read(sp, table);
            if (reader == null) return;

            memos.Clear();
            while (reader.Read())
            {
                var memo = new Memo
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
                };

                // Check if there's an image column and it's not null
                if (reader.FieldCount > 13 && !reader.IsDBNull(13))
                {
                    memo.MemoImage = reader.GetString(13);
                }

                memos.Add(memo);
            }
            reader.CloseAsync();
        }

        private int ReadPodRolePerson(string role)
		{
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

		



        //The selected sender message are filtered from all the memos
        private void FilterMemosByPid(int senderPid)
        {
            currentSenderMemo.Clear(); // Ensure the list is empty before adding new memos
            currentSenderMemo = memos.Where(m => m.MemoPersonFrom == senderPid && m.MemoPersonTo == pid).ToList();


        }

       


        private void ChangeInbox(ChangeEventArgs e)
        {
            DateTime dt = DateTime.Parse(e.Value.ToString());
            Memo currentMemo = memos.Find(memo => memo.MemoDateTime.ToString() == dt.ToString());
            message = currentMemo.MemoMessage;
            messageType = currentMemo.MemoType;
            currentImage = currentMemo.MemoImage; // Store the current image to display
        }

        private void OnRoleChanged(ChangeEventArgs e)
        {
            roleRecipient = "self";
            int senderPid = 0;

            var selectedValue = e.Value?.ToString(); // e.g., "user|103"
            if (!string.IsNullOrWhiteSpace(selectedValue) && selectedValue.Contains('|'))
            {
                var parts = selectedValue.Split('|');
                roleRecipient = parts[0]; // e.g., "user"
                if (int.TryParse(parts[1], out int parsedPid))
                {
                    senderPid = parsedPid;
                }
            }

            person1 = senderPid;
            FilterMemosByPid(senderPid);

            if (currentSenderMemo.Any())
            {
                message = currentSenderMemo.Last().MemoMessage;
                messageType = currentSenderMemo.Last().MemoType;
                currentImage = currentSenderMemo.Last().MemoImage; // Set the image
                MessageTime = currentSenderMemo.Last().MemoDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                message = "No messages found.";
                messageType = "None";
                currentImage = null; // Clear the image
                MessageTime = "None";
            }
        }
        private void ShowPreviousMemo()
        {
            if (currentSenderMemo.Count == 0)
            {
                message = "no memos to show";
                currentImage = null; // Clear image when no memos
                return; // Return if no memos
            }

            if (currentMemoIndex == -1)
                currentMemoIndex = currentSenderMemo.Count - 1; // Start from the latest memo

            // Update the message with the current memo
            person1 = currentSenderMemo[currentMemoIndex].MemoPersonFrom;
            message = currentSenderMemo[currentMemoIndex].MemoMessage;
            messageType = currentSenderMemo[currentMemoIndex].MemoType;
            currentImage = currentSenderMemo[currentMemoIndex].MemoImage; // Set the image
            MessageTime = currentSenderMemo.Any() ? currentSenderMemo[currentMemoIndex].MemoDateTime.ToString("yyyy-MM-dd HH:mm:ss") : "None";

            // Move to the previous memo
            currentMemoIndex--;
            // Reset index if we reach the first memo
            if (currentMemoIndex < 0)
                currentMemoIndex = currentSenderMemo.Count - 1; // Loop back to the last memo
        }

        //******************************* stored proceedure **************************************
        protected override void OnInitialized()
		{
			LoadMemos("Read_Grid", "Memo");
		}


    }
}