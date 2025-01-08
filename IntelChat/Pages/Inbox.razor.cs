using IntelChat.Models;
using IntelChat.Services;
using Microsoft.AspNetCore.Components;
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
		public string message = "";
		public string messageType = "";
		private List<Memo> memos = new List<Memo>();
		public string roleRecipient = "";

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
		//******************************* stored proceedure **************************************
		protected override void OnInitialized()
		{
			LoadMemos("Read_Grid", "Memo");
		}
	}
}