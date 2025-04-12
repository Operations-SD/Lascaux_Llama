namespace IntelChat.Services
{
    public class InterviewInboxStateService
    {
        public event Action? OnShowInterviewInbox;
        public void ShowInterviewInbox() => OnShowInterviewInbox?.Invoke();
    }
}
