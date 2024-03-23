namespace ScrumAndCo.Domain.Sprints;

public class ReviewSprint : Sprint
{
    internal override void AcceptVisitor(ISprintVisitor visitor)
    {
        visitor.AcceptReview(this);
    }

    public ReviewSprint(string name, string description, DateOnly activeFrom, DateOnly activeUntil, Project project) : base(name, description, activeFrom, activeUntil, project)
    {
    }
}