namespace ScrumAndCo.Domain.Sprint.State;

public class OngoingState : SprintState
{
    public OngoingState(Sprint context) : base(context)
    {
    }

    public override void NextSprintState()
    {
        _context.ChangeSprintState(new FinishedState(_context));
    }
}