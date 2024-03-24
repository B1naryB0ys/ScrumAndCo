using ScrumAndCo.Domain.Exceptions;

namespace ScrumAndCo.Domain.Forum.States;

public abstract class ThreadState
{
    internal Thread _context;
    

    public ThreadState(Thread context)
    {
        _context = context;
    }
    public abstract void NextThreadState();

    public virtual void AddComment(Comment comment)
    {
        throw new IllegalStateActionException("Cannot add comment to a thread that is not open.");
    }
}