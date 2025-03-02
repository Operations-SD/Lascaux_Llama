using System;
using System.Threading.Tasks;

public class ChatStateService
{
    public string RoleRecipient { get; set; } = "";
    public string ReplyMessage { get; set; } = "";
    public event Action? OnShowResponse;

    public void ShowResponse()
    {
        OnShowResponse?.Invoke();
        
    }
}
