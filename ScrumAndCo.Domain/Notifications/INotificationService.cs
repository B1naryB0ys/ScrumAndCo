namespace ScrumAndCo.Domain.Notifications;

public interface INotificationService
{
    public void SendMessage(string message);
}