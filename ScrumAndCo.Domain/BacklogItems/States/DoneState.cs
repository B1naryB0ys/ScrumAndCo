using ScrumAndCo.Domain.Forum.States;

namespace ScrumAndCo.Domain.BacklogItems.States;

public class DoneState : ItemState
{
    public DoneState(BacklogItem context) : base(context)
    {
        // Move all threads to finished state
        foreach (var thread in _context.Threads)
        {
            thread.ChangeThreadState(new ArchivedState(thread));
        }
    }
}