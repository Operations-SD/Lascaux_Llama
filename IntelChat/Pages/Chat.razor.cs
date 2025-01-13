using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using IntelChat.Models;
using IntelChat.Hubs;
using Task = System.Threading.Tasks.Task;
using System.Security.Claims;

namespace IntelChat.Pages
{
    public partial class Chat
    {
        private HubConnection hubConnection;
        private List<UserMessage> userMessages = new();
        private string usernameInput;
        private string messageInput;
        private bool isUserReadonly = false;

        public bool IsConnected => hubConnection.State == HubConnectionState.Connected;

        protected override async Task OnInitializedAsync()
        {
			usernameInput = context.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "";

			hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/chathub"))
                .Build();

            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                InvokeAsync(() =>
                {
                    userMessages.Add(new UserMessage
                    {
                        Username = user,
                        Message = message,
                        CurrentUser = user == usernameInput,
                        DateSent = DateTime.Now
                    });

                    StateHasChanged();
                });
            });

            await hubConnection.StartAsync();
        }

        private async Task Send()
        {
            if (!string.IsNullOrEmpty(usernameInput) && !string.IsNullOrEmpty(messageInput))
            {
                await hubConnection.SendAsync("SendMessage", usernameInput, messageInput);

                isUserReadonly = true;
                messageInput = string.Empty;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (hubConnection is not null)
            {
                await hubConnection.DisposeAsync();
            }
        }
    }
}