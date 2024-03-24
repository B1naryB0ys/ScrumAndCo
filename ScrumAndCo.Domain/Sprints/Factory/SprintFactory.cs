using ScrumAndCo.Domain.Notifications;

namespace ScrumAndCo.Domain.Sprints;
public class SprintFactory : ISprintFactory
{
    public Sprint CreateSprint(string name, string description, DateOnly startDate, DateOnly endDate , Project project, Pipeline.Pipeline pipeline, ISubject<string> notificationSubject, SprintType sprintType)
    {
        return sprintType switch
        {
            SprintType.Release => new ReleaseSprint(name, description, startDate, endDate, project, pipeline, notificationSubject),
            SprintType.Review => new ReviewSprint(name, description, startDate, endDate, project, pipeline, notificationSubject),
            _ => throw new InvalidOperationException("Invalid sprint type.")
        };
    }
}