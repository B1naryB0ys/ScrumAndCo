using ScrumAndCo.Domain.BacklogItems;

namespace ScrumAndCo.Domain.Sprints.States;

public class PlanningState(Sprint context) : SprintState(context)
{
    public override void NextSprintState()
    {
        _context.ChangeSprintState(new OngoingState(_context));
    }

    // The user is allowed to change the properties of the sprint in the planning state
    public override void ChangeProperties(string name, string description, DateOnly startDate, DateOnly endDate)
    {
        _context.Name = name;
        _context.Description = description;
        _context.ActiveFrom = startDate;
        _context.ActiveUntil = endDate;
    }
    
    // The user is allowed to add a backlog item to the sprint in the planning state
    public override void AddBacklogItem(BacklogItem backlogItem)
    {
        _context.BacklogItems.Add(backlogItem);
    }
}