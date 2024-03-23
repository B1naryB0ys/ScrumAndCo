namespace ScrumAndCo.Domain.Sprints;

public interface ISprintVisitor
{
    public void AcceptReview(ReviewSprint sprint);
    public void AcceptRelease(ReleaseSprint sprint);
}