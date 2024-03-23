using ScrumAndCo.Domain.Sprints;

namespace ScrumAndCo.Domain.BacklogItems.States;

public class InProgressState(BacklogItem context) : ItemState(context)
{
    public override void ToReadyForTesting()
    {
        _context.ToReadyForTestingState();
    }
}