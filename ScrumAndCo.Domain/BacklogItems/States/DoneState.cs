namespace ScrumAndCo.Domain.BacklogItems.States;

public class DoneState(BacklogItem context) : ItemState(context)
{
    public override void OnStateEnter()
    {
        Console.WriteLine("Backlog item is in Done state");
    }
}