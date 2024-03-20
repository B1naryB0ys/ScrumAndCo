using ScrumAndCo.Domain.Sprints;

namespace ScrumAndCo.Domain.BacklogItems.States;

public class InProgressState(BacklogItem context) : ItemState(context)
{
    public override void OnStateEnter()
    {
        Console.WriteLine("Backlog item is in InProgress state");
    }

    public override string GetFullName()
    {
        return "In Progress State";
    }
}