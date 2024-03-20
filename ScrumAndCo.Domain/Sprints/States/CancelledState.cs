using ScrumAndCo.Domain.BacklogItems.States;
using ScrumAndCo.Domain.Exceptions;

namespace ScrumAndCo.Domain.Sprints.States;

public class CancelledState(Sprint context) : SprintState(context)
{
    public override void NextSprintState()
    {
        throw new IllegalStateException("This sprint is cancelled. You can't change its state.");
    }
}