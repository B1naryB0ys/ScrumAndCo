namespace ScrumAndCo.Domain.BacklogItems.States;

public class TestingState(BacklogItem context) : ItemState(context)
{
    public override void ToTodo()
    {
        // A problem was found in the BacklogItem class, move the item to the TodoState (work for developer)
        _context.ChangeState(new TodoState(_context));
        
        // Notify the scrum master that the item is back in the todo state
        _context._notificationSubject.NotifySingle($"The item {_context.Name} is back in the todo state", _context.Project.GetProjectScrumMaster());
    }
    
    public override void ToTested()
    {
        _context.ChangeState(new TestedState(_context));
    }
}