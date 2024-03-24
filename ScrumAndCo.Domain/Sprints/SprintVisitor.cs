using ScrumAndCo.Domain.Sprints.States;

namespace ScrumAndCo.Domain.Sprints;

public class SprintVisitor : ISprintVisitor
{
    public void AcceptReview(ReviewSprint sprint)
    {
        sprint.ChangeSprintState(new ReviewState(sprint));
    }

    public void AcceptRelease(ReleaseSprint sprint)
    {
        sprint.ChangeSprintState(new ReleaseState(sprint));
    }
}