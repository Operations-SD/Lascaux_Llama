using IntelChat.Models;
using IntelChat.Services;
using Microsoft.AspNetCore.Components;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using ThreadingTask = System.Threading.Tasks.Task;

namespace IntelChat.Pages
{
	public partial class Page_Home
	{
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pod { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pid { get; set; }
		public string role { get; set; }
		public bool modal = false;
		public bool showImage = true;
		public string show = "";
		public string name = "";
		public string message = "";
		public string roleRecipient = "";
		public string messageType = "";
		public Pod currentPod = new Pod();
		
		[Inject]
		public NotificationService NotificationService { get; set; }

		public string GetMenuPath(string role)
		{
			return role switch
			{
				"acad" => "Academy",
				"admn" => "Administration",
				"ainn" => "Nnet",
				"engr" => "Engineer",
				"user" => "User",
				"xprt" => "Expert",
				_ => ""
			};
		}

		public void HideModal() => modal = false;
		public void ShowModal() => modal = true;
		public void ShowChat() { ShowModal(); show = "chat"; }
		public async void ShowMemo() { ShowModal(); show = "memo"; }
		public void ShowVideo() { ShowModal(); show = "video"; }
		public void ShowAudio() { ShowModal(); show = "audio"; }
		public void ShowProject() { ShowModal(); show = "project"; }

		public void NavigateToMenu()
		{
			_nav.NavigateTo($"/{role}?pod={pod}&pid={pid}", true);
		}

		public void NavigateToCommunity()
		{
			_nav.NavigateTo($"/Community", true);
		}

		private async Task<SqlDataReader?> ExecuteStoredProcedure(string procedure, List<SqlParameter> parameters, bool reader = false)
		{
			var connection = new SqlConnection(_configuration.GetValue<string>("ConnectionStrings:DefaultConnection"));
			using var command = new SqlCommand(procedure, connection) { CommandType = CommandType.StoredProcedure };
			parameters.ForEach(parameter => command.Parameters.Add(parameter));
			await connection.OpenAsync();
			if (reader) return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
			await command.ExecuteNonQueryAsync();
			return null;
		}

		private async ThreadingTask ReadPOD()
		{
            /// *********************************************************************************
            /// ******************** Input Parmeters ****** Read POD ****************************
            /// *********************************************************************************
			/// 
            List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@PROC_action", "Read"),
				new SqlParameter("@pod_id", 3),
				new SqlParameter("@pod_status", "*")
			};
			var reader = await ExecuteStoredProcedure("dbo.[CRUD_POD]", parameters, true);
			if (reader == null) return;
			await reader.ReadAsync();
			currentPod = new Pod
			{
				PodId = reader.GetInt32(0),
				PodLabel = reader.GetString(1),
				PodDescription = reader.GetString(2),
				PodType = reader.GetString(3),
				PodStatus = reader.GetString(4),
				PodChannel = reader.GetString(5),
				PodUrlBase = reader.GetString(6),
				PersonIdFk = reader.GetInt32(7),
				LocationIdFk = reader.GetInt32(8),
				NovaIdFk = reader.GetInt32(9)
			};
			await reader.CloseAsync();
		}
        /// *********************************************************************************
        /// ******************** Input Parmeters ****** Read POD ****************************
        /// *********************************************************************************
        protected async override ThreadingTask OnInitializedAsync()
		{
			name = context.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "";
			pod = Int32.Parse(context.HttpContext?.User.FindFirst(ClaimTypes.Locality)?.Value ?? "0");
			pid = Int32.Parse(context.HttpContext?.User.FindFirst(ClaimTypes.Actor)?.Value ?? "0");
			role = GetMenuPath(context.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value.Split("-")[1] ?? "");
			if(pod != 0 && pid != 0) await ReadPOD();
		}
	}
}