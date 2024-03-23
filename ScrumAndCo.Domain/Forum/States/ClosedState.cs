using ScrumAndCo.Domain.Exceptions;

namespace ScrumAndCo.Domain.Forum.States;

public class ClosedState (Thread context) : ThreadState(context)
{
    public override void NextThreadState()
    {
        // TODO: Move thread to the project "forum" through the context
    }
}