using ScrumAndCo.Domain.BacklogItems;
using ScrumAndCo.Domain.Exceptions;

namespace ScrumAndCo.Domain.Sprints.States;

public abstract class SprintState
{
    internal Sprint _context;

    public SprintState(Sprint context)
    {
        _context = context;
    }
    public abstract void NextSprintState();

    public virtual void ChangeProperties(string name, string description, DateOnly startDate, DateOnly endDate)
    {
        throw new IllegalStateActionException("Cannot change properties of the sprint in this state");
    }
    
    public virtual void AddBacklogItem(BacklogItem backlogItem)
    {
        throw new IllegalStateActionException("Cannot add backlog items to the sprint in this state");
    }
}