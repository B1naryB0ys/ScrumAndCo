namespace ScrumAndCo.Domain.Sprint.State;

public class PlanningState : SprintState
{
    public PlanningState(Sprint context) : base(context)
    {
    }

    public override void NextSprintState()
    {
        _context.ChangeSprintState(new OngoingState(_context));
    }
}