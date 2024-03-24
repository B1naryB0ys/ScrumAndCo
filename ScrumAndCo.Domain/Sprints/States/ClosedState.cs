using ScrumAndCo.Domain.Exceptions;

namespace ScrumAndCo.Domain.Sprints.States;

public class ClosedState(Sprint context) : SprintState(context)
{
    public override void NextSprintState()
    {
        throw new IllegalStateException("This sprint is closed. You can't change its state.");
    }
}