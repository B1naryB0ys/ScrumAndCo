using ScrumAndCo.Domain.Notifications;

namespace ScrumAndCo.Domain.Sprints;

public interface ISprintFactory
{
    public Sprint CreateSprint(string name, string description, DateOnly startDate, DateOnly endDate , Project project, Pipeline.Pipeline pipeline, ISubject<string> notificationSubject, SprintType sprintType);
}