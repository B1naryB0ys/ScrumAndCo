namespace ScrumAndCo.Domain.Sprints.States;

public class OngoingState(Sprint context) : SprintState(context)
{
    public override void NextSprintState()
    {
        _context.ChangeSprintState(new FinishedState(_context));
    }
}