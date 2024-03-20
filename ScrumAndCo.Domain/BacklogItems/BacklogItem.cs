using ScrumAndCo.Domain.BacklogItems.States;

namespace ScrumAndCo.Domain.BacklogItems;

public class BacklogItem
{
    private ItemState _backlogItemState;
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public BacklogItem(string name, string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        _backlogItemState = new TodoState(this);
    }
    
    public void ChangeItemState(ItemState itemState)
    {
        Console.WriteLine($"New backlog item state: {itemState.GetFullName()}");
        _backlogItemState = itemState;
    }
}