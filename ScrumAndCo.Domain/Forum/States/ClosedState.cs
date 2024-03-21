using ScrumAndCo.Domain.Exceptions;

namespace ScrumAndCo.Domain.Forum.States;

public class ClosedState (Thread context) : ThreadState(context)
{
    public override void NextThreadState()
    {
        throw new IllegalStateException("The thread is already closed. You can't change its state.");
    }
}