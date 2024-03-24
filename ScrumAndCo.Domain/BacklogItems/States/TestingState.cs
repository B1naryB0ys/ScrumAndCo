namespace ScrumAndCo.Domain.BacklogItems.States;

public class TestingState(BacklogItem context) : ItemState(context)
{
    public override void ToTodo()
    {
        // A problem was found in the BacklogItem class, move the item to the TodoState (work for developer)
        context.ToTodoState();
    }
    
    public override void ToTested()
    {
        _context.ToTestedState();
    }
}