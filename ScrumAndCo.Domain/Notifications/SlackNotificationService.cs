namespace ScrumAndCo.Domain.Notifications;

public class SlackNotificationService : INotificationService
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"Sending slack notification: {message}");
    }
}