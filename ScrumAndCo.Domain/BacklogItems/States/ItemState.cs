using ScrumAndCo.Domain.Exceptions;

namespace ScrumAndCo.Domain.BacklogItems.States;

public abstract class ItemState
{
    protected BacklogItem _context;
    
    public ItemState(BacklogItem context)
    {
        _context = context;
        Console.WriteLine($"Backlog item {_context.Name} is in {this.GetType().Name} state");
    }

    public virtual void ToTodo()
    {
        throw new IllegalStateException("Changing from this state is not allowed");
    }
    
    public virtual void ToInProgress()
    {
        throw new IllegalStateException("Changing from this state is not allowed");
    }
    
    public virtual void ToReadyForTesting()
    {
        throw new IllegalStateException("Changing from this state is not allowed");
    }
    
    public virtual void ToTesting()
    {
        throw new IllegalStateException("Changing from this state is not allowed");
    }
    
    public virtual void ToTested()
    {
        throw new IllegalStateException("Changing from this state is not allowed");
    }
    
    public virtual void ToDone(User user)
    {
        throw new IllegalStateException("Changing from this state is not allowed");
    }
}