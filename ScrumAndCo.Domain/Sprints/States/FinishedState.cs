using ScrumAndCo.Domain.Exceptions;

namespace ScrumAndCo.Domain.Sprints.States;

public class FinishedState(Sprint context) : SprintState(context)
{
    public override void NextSprintState()
    {
        throw new IllegalStateException("The sprint is already finished. You can't change its state.");
    }
}