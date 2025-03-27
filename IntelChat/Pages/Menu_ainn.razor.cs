using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace IntelChat.Pages
{
	public partial class Menu_ainn
	{

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

        /*
			************************************************************************************
			******************************** INPUT PARAMETERS **********************************
			************************************************************************************
		 */
        [Parameter]
		[SupplyParameterFromQuery]
		public int? pod { get; set; }
		[Parameter]
		[SupplyParameterFromQuery]
		public int? pid { get; set; }


        protected override void OnInitialized()
        {

            ResponseService.OnShowResponse += ShowResponse;
            NewMemoService.OnShowNewMemo += ShowNewMemo;

        }




        public void HideModal() => modal = false;
        public void ShowModal() => modal = true;


        /**********************************************/
        /***************  IFRAMES *********************/
        /**********************************************/

        [Inject] public ChatStateService ResponseService { get; set; }
        [Inject] public NewStateService NewMemoService { get; set; }



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

    }
}