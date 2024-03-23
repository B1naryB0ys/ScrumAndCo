namespace ScrumAndCo.Domain.Sprints;

public class ReviewSprint : Sprint
{
    internal override void AcceptVisitor(ISprintVisitor visitor)
    {
        visitor.AcceptReview(this);
    }
}