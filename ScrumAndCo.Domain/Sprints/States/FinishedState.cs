using ScrumAndCo.Domain.Exceptions;

namespace ScrumAndCo.Domain.Sprints.States;

public class FinishedState(Sprint context) : SprintState(context)
{
    public override void NextSprintState()
    {
        ISprintVisitor visitor = new SprintVisitor();
        _context.AcceptVisitor(visitor);
    }
}