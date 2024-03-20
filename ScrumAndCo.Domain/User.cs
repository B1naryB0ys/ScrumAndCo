using ScrumAndCo.Domain.Notifications;

namespace ScrumAndCo.Domain;

public class User : Notifications.IObserver<string>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateOnly BirthDate { get; set; }
    
    public IList<INotificationService> NotificationPreferences { get; set; } = new List<INotificationService>();
    
    public void Update(string value)
    {
        foreach (var preferencedNotification in NotificationPreferences)
        {
            preferencedNotification.SendMessage(value);
        }
    }
    
    public void AddNotificationPreference(INotificationService notificationService)
    {
        NotificationPreferences.Add(notificationService);
    }
}