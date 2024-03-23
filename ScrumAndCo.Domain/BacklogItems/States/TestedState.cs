using ScrumAndCo.Domain.Exceptions;

namespace ScrumAndCo.Domain.BacklogItems.States;

public class TestedState(BacklogItem context) : ItemState(context)
{
    public override void ToDone(User user)
    {
        if(!context.Project.CanUserMoveBacklogItemToDone(user)) {
            throw new IllegalStateActionException("User does not have permission to move item to Done state");
        }
        
        _context.ToDoneState();
    }

    public override void ToReadyForTesting()
    {
        _context.ToReadyForTestingState();
    }
}