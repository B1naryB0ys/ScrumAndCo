namespace ScrumAndCo.Domain.Sprints;

public class SprintVisitor : ISprintVisitor
{
    public void AcceptReview(ReviewSprint sprint)
    {
        sprint.AcceptVisitor(this);
    }

    public void AcceptRelease(ReleaseSprint sprint)
    {
        sprint.AcceptVisitor(this);
    }
}