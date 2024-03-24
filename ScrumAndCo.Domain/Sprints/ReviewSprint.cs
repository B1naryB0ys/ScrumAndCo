using ScrumAndCo.Domain.Notifications;

namespace ScrumAndCo.Domain.Sprints;

public class ReviewSprint : Sprint
{
    internal override void AcceptVisitor(ISprintVisitor visitor)
    {
        visitor.AcceptReview(this);
    }

    public ReviewSprint(string name, string description, DateOnly activeFrom, DateOnly activeUntil, Project project, Pipeline.Pipeline pipeline, ISubject<string> notificationSubject) : base(name, description, activeFrom, activeUntil, project, pipeline, notificationSubject)
    {
    }
    
    public void AddReviewFileUrl(string reviewFileUrl)
    {
        _sprintState.UploadReview(reviewFileUrl);
    }
}