namespace ScrumAndCo.Domain.Sprints;

public class ReleaseSprint : Sprint
{
    internal override void AcceptVisitor(ISprintVisitor visitor)
    {
        visitor.AcceptRelease(this);
    }
}