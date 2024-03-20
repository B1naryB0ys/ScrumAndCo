namespace ScrumAndCo.Domain.BacklogItems.States;

public abstract class ItemState
{
    protected BacklogItem _context;
    
    
    public ItemState(BacklogItem context)
    {
        _context = context;
    }
    
    public abstract void OnStateEnter();
}