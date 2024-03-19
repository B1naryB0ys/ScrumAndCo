using ScrumAndCo.Domain.Exceptions;

namespace ScrumAndCo.Domain.Sprint.State;

public class FinishedState : SprintState
{
    public FinishedState(Sprint context) : base(context)
    {
    }

    public override void NextSprintState()
    {
        throw new IllegalStateException("The sprint is already finished. You can't change its state.");
    }
}