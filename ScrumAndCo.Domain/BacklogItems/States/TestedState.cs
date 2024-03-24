using ScrumAndCo.Domain.Exceptions;

namespace ScrumAndCo.Domain.BacklogItems.States;

public class TestedState(BacklogItem context) : ItemState(context)
{
    public override void ToDone(User user)
    {
        if(!context.Project.CanUserMoveBacklogItemToDone(user)) {
            throw new IllegalStateActionException("User does not have permission to move item to Done state");
        }

        if (!context.AllTasksCompleted())
        {
            throw new IllegalStateActionException("All tasks must be completed before moving to Done state");
        }
        
        _context.ChangeState(new DoneState(_context));
    }

    public override void ToReadyForTesting()
    {
        _context.ChangeState(new ReadyForTestingState(_context));
    }
}