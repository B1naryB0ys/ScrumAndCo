namespace ScrumAndCo.Domain.BacklogItems.States;

public class ReadyForTestingState : ItemState
{
    public ReadyForTestingState(BacklogItem context) : base(context)
    {
        var testers = context.Project.GetProjectTesters();
        if (testers.Count > 0)
        {
            foreach (var tester in testers)
            {
                // Notify the tester that the item is ready for testing
                if (tester != null) _context._notificationSubject.Attach(tester);
            }
        }
        
        _context._notificationSubject.NotifyAll($"{_context.Name} is ready for testing");
    }

    public override void ToTesting()
    {
        _context.ToTestingState();
    }
}