using ScrumAndCo.Domain.BacklogItems.States;
using ScrumAndCo.Domain.Notifications;
using ScrumAndCo.Domain.Sprints;

namespace ScrumAndCo.Domain.BacklogItems;

public class BacklogItem
{
    private ItemState _backlogItemState;
    
    internal NotificationSubject<string> NotificationSubject = new NotificationSubject<string>();
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public Project Project { get; set; }
    public Sprint? Sprint { get; set; }
    
    public BacklogItem(string name, string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        _backlogItemState = new TodoState(this);
    }
    
    public void ChangeItemState(ItemState itemState)
    {
        Console.WriteLine($"New backlog item state: {itemState.GetType().Name}");
        _backlogItemState = itemState;
    }
    
    public void Subscribe(User user)
    {
        NotificationSubject.Attach(user);
    }
}