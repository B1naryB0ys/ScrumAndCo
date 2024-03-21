namespace ScrumAndCo.Domain.Forum.States;

public abstract class ThreadState
{
    internal Thread _context;

    public ThreadState(Thread context)
    {
        _context = context;
    }
    public abstract void NextThreadState();
}