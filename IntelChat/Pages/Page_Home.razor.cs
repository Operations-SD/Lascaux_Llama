using IntelChat.Models;
using IntelChat.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
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
		public bool isChatIframeVisible = false; // Manages the visibility of the Chat iframe
		public bool isMemoIframeVisible = false; // Manages the visibility of the Memo iframe
        public bool isResponseIframeVisible = false;//manages at the response of the memo
        public bool isNewMemoIframeVisible = false;//manages  the new memo creation
        private bool isDraggingMemo = false;
        private bool isDraggingNewMemo = false;
        private (int X, int Y) memoIframePosition = (0, 700);
		private (int X, int Y) mouseStartMemo = (0, 0);
		private bool isMemoInteractive = true;
		private bool isDraggingChat = false;
        private bool isDraggingResponse = false;
        private (int X, int Y) newMemoIframePosition = (600, 60);
        private (int X, int Y) mouseStartNewMemo = (0, 0);
        private (int X, int Y) responseIframePosition = (500, 60);
        private (int X, int Y) mouseStartResponse = (0, 0);
        private (int X, int Y) chatIframePosition = (1710, 60);
		private (int X, int Y) mouseStartChat = (0, 0);
		public string ChatiframeSource = "";      // Holds the source of the iframe to be displayed
		public string MemoiframeSource = "";
        public string ResponseiframeSource = "";//for response iframe
        public string NewMemoiframeSource = "";//for response iframe
        public string CreateMemoiframeSource = ""; //for create new memo iframe
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


        /**********************************************/
        /***************  IFRAMES *********************/
        /**********************************************/

        [Inject] public ChatStateService ResponseService { get; set; }
        [Inject] public NewStateService NewMemoService { get; set; }

        protected override void OnInitialized()
        {
            ResponseService.OnShowResponse += ShowResponse;
			NewMemoService.OnShowNewMemo += ShowNewMemo;
        }

        [Inject] NavigationManager NavigationManager { get; set; }
        public void ShowResponse()
        {
            ResponseiframeSource = "/Response";  // Set the source of the iframe (Response page)
            isResponseIframeVisible = true;  // Show the iframe
            StateHasChanged();
        }
        public void HideResponse()
        {
            isResponseIframeVisible = false; // Hide the iframe
            ResponseiframeSource = "";       // Clear the iframe source
        }
        public void ShowChat()
        {
            ChatiframeSource = "/Chat";  // Set the source of the iframe (chat page)
            isChatIframeVisible = true;  // Show the iframe
            StateHasChanged();  // Ensure UI updates
        }
        public void HideChat()
		{
			isChatIframeVisible = false; // Hide the iframe
			ChatiframeSource = "";       // Clear the iframe source
		}

		public void ShowMemo()
		{
			MemoiframeSource = "/Inbox";// Set iframe source to Inbox page
			isMemoIframeVisible = true;
		}
		public void HideMemo()
		{
			isMemoIframeVisible = false; // Hide the iframe
			MemoiframeSource = "";       // Clear the iframe source
		}

        public void ShowNewMemo()
        {
            NewMemoiframeSource = "/NewMemo";// Set iframe source to New Memo page
            isNewMemoIframeVisible = true;
            InvokeAsync(StateHasChanged);
        }
        public void HideNewMemo()
        {
            isNewMemoIframeVisible = false; // Hide the iframe
            NewMemoiframeSource = "";       // Clear the iframe source
            StateHasChanged();
        }

        /*********************************************************************************
		 ************************ Draggable IFrames **************************************
		 *********************************************************************************/

        // Initiates dragging for the Chat iframe

        private void StartDraggingResponse(MouseEventArgs e)
        {
            isDraggingResponse = true; // Set dragging state to true for the Chat iframe
            mouseStartResponse = ((int)e.ClientX - responseIframePosition.X, (int)e.ClientY - responseIframePosition.Y); // Calculate initial offset
        }
        private void StartDraggingChat(MouseEventArgs e)
		{
			isDraggingChat = true; // Set dragging state to true for the Chat iframe
			mouseStartChat = ((int)e.ClientX - chatIframePosition.X, (int)e.ClientY - chatIframePosition.Y); // Calculate initial offset
		}

		// Initiates dragging for the Memo iframe
		private void StartDraggingMemo(MouseEventArgs e)
		{
			isDraggingMemo = true; // Set dragging state to true for the Memo iframe
			mouseStartMemo = ((int)e.ClientX - memoIframePosition.X, (int)e.ClientY - memoIframePosition.Y); // Calculate initial offset
		}

        // Initiates dragging for the Memo iframe
        private void StartDraggingNewMemo(MouseEventArgs e)
        {
            isDraggingNewMemo = true; // Set dragging state to true for the Memo iframe
            mouseStartNewMemo = ((int)e.ClientX - newMemoIframePosition.X, (int)e.ClientY - newMemoIframePosition.Y); // Calculate initial offset
        }


        // Handles the mouse move event globally for dragging
        private void OnMouseMoveGlobal(MouseEventArgs e)
		{
			if (isDraggingMemo)
			{
				// Update position of the Memo iframe based on the current mouse position
				memoIframePosition = ((int)e.ClientX - mouseStartMemo.X, (int)e.ClientY - mouseStartMemo.Y);
				StateHasChanged(); // Refresh the UI to reflect position changes
			}
			else if (isDraggingChat)
			{
				// Update position of the Chat iframe based on the current mouse position
				chatIframePosition = ((int)e.ClientX - mouseStartChat.X, (int)e.ClientY - mouseStartChat.Y);
				StateHasChanged(); // Refresh the UI to reflect position changes
			}
            else if (isDraggingResponse)
            {
                // Update position of the Chat iframe based on the current mouse position
                responseIframePosition = ((int)e.ClientX - mouseStartResponse.X, (int)e.ClientY - mouseStartResponse.Y);
                StateHasChanged(); // Refresh the UI to reflect position changes
            }
            else if (isDraggingNewMemo)
            {
                // Update position of the Chat iframe based on the current mouse position
                newMemoIframePosition = ((int)e.ClientX - mouseStartNewMemo.X, (int)e.ClientY - mouseStartNewMemo.Y);
                StateHasChanged(); // Refresh the UI to reflect position changes
            }
        }

		// Ends the dragging operation globally
		private void OnMouseUpGlobal()
		{
			if (isDraggingMemo)
			{
				isDraggingMemo = false; // Reset dragging state for the Memo iframe
			}
			else if (isDraggingChat)
			{
				isDraggingChat = false; // Reset dragging state for the Chat iframe
			}
            else if (isDraggingResponse)
            {
                isDraggingResponse = false; // Reset dragging state for the Response iframe
            }
            else if (isDraggingNewMemo)
            {
                isDraggingNewMemo = false; // Reset dragging state for the Response iframe
            }
        }



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
				new SqlParameter("@PROC_action", "Home"),
				new SqlParameter("@pod_id", pod),
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
				PodPypeDdChan = reader.GetString(5),
				PodImage = reader.GetString(6),
				PersonIdFk = reader.GetInt32(11),
				LocationIdFk = reader.GetInt32(12),
				NovaIdFk = reader.GetInt32(14)
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