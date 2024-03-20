namespace ScrumAndCo.Domain.BacklogItems.States;

public class TestedState(BacklogItem context) : ItemState(context)
{
    public override void OnStateEnter()
    {
        Console.WriteLine("Backlog item is in Tested state");
    }

    public override string GetFullName()
    {
        return "Tested State";
    }
}