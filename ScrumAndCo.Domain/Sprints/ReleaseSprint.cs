using ScrumAndCo.Domain.Notifications;

namespace ScrumAndCo.Domain.Sprints;

public class ReleaseSprint : Sprint
{
    internal override void AcceptVisitor(ISprintVisitor visitor)
    {
        visitor.AcceptRelease(this);
    }

    public ReleaseSprint(string name, string description, DateOnly activeFrom, DateOnly activeUntil, Project project, Pipeline.Pipeline pipeline, ISubject<string> notificationSubject) : base(name, description, activeFrom, activeUntil, project, pipeline, notificationSubject)
    {
    }
}