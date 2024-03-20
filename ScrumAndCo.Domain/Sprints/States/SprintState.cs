namespace ScrumAndCo.Domain.Sprints.States;

public abstract class SprintState
{
    internal Sprint _context;

    public SprintState(Sprint context)
    {
        _context = context;
    }

    public abstract void NextSprintState();
}