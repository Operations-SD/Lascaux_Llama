using Microsoft.AspNetCore.Components;

namespace IntelChat.Shared
{
    public partial class ChatLayout : LayoutComponentBase
    {
        [Inject] protected NavigationManager NavigationManager { get; set; }

        // Any additional logic for the layout can go here
        protected override void OnInitialized()
        {
            base.OnInitialized();
            // Initialization code for layout if needed
        }
    }
}
