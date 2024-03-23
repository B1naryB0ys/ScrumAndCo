namespace ScrumAndCo.Domain.Forum.States;

public class ArchivedState (Thread context) : ThreadState(context)
{
    public override void NextThreadState()
    {
        // TODO: Move thread to the project "archive" through the context
    }
}