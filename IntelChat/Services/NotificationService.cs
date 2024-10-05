namespace IntelChat.Services
{
	public class NotificationService
	{
		public event Action<string, NotificationType> OnNotify;

		public void Notify(string message, NotificationType type = NotificationType.Info)
		{
			OnNotify?.Invoke(message, type);
		}
	}

	public enum NotificationType
	{
		Info,
		Success,
		Error,
	}
}
