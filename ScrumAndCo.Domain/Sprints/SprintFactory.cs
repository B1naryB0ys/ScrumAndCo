namespace ScrumAndCo.Domain.Sprints;
public static class SprintFactory
{
    public static Sprint CreateSprint(string name, string description, DateOnly startDate, DateOnly endDate , Project project, SprintTypes sprintType)
    {
        return sprintType switch
        {
            SprintTypes.Release => new ReleaseSprint(name, description, startDate, endDate, project),
            SprintTypes.Review => new ReviewSprint(name, description, startDate, endDate, project),
            _ => throw new InvalidOperationException("Invalid sprint type.")
        };
    }
}