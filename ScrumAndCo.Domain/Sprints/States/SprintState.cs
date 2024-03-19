namespace ScrumAndCo.Domain.Sprint.State;

public abstract class SprintState
{
    protected Sprint _context;

    public SprintState(Sprint context)
    {
        _context = context;
    }

    public abstract void NextSprintState();
}