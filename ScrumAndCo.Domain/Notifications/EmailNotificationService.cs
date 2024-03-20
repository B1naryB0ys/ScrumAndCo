namespace ScrumAndCo.Domain.Notifications;

public class EmailNotificationService : INotificationService
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"Sending email notification: {message}");
    }
}