namespace ScrumAndCo.Domain.BacklogItems.States;

public class TestingState(BacklogItem context) : ItemState(context)
{
    public override void OnStateEnter()
    {
        Console.WriteLine("Backlog item is in Testing state");
    }
}