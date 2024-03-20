namespace ScrumAndCo.Domain.Sprints.States;

public class PlanningState(Sprint context) : SprintState(context)
{
    public override void NextSprintState()
    {
        _context.ChangeSprintState(new OngoingState(_context));
    }
}