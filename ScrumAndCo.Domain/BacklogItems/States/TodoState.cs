namespace ScrumAndCo.Domain.BacklogItems.States;

public class TodoState(BacklogItem context) : ItemState(context)
{
    public override void OnStateEnter()
    {
        Console.WriteLine("Backlog item is in Todo state");
    }

    public override string GetFullName()
    {
        return "Todo State";
    }
}