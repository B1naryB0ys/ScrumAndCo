namespace ScrumAndCo.Domain.BacklogItems.States;

public class ReadyForTestingState(BacklogItem context) : ItemState(context)
{
    public override void OnStateEnter()
    {
        Console.WriteLine("Backlog item is in ReadyForTesting state");
    }
}