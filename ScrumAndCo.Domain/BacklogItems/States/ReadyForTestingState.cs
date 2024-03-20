namespace ScrumAndCo.Domain.BacklogItems.States;

public class ReadyForTestingState(BacklogItem context) : ItemState(context)
{
    public override void OnStateEnter()
    {
        // TODO: Notify the TESTERS and PRODUCT_OWNER when a backlog item is ready for testing
        context.NotificationSubject.Notify($"Backlog item {context.Name} is ready for testing");
        
        Console.WriteLine("Backlog item is in ReadyForTesting state");
    }
}