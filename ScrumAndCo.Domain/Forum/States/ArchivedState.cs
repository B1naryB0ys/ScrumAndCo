using ScrumAndCo.Domain.Exceptions;

namespace ScrumAndCo.Domain.Forum.States;

public class ArchivedState : ThreadState
{
    public ArchivedState(Thread context) : base(context)
    {
        // Move thread to project forum
        _context.BacklogItem.Project.AddThreadToForum(context);
        _context.BacklogItem.RemoveThread(context);
    }
    
    public override void NextThreadState()
    {
        throw new IllegalStateException("Cannot change state of a archived thread.");
    }
}