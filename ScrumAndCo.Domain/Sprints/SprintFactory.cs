namespace ScrumAndCo.Domain.Sprints;
public static class SprintFactory
{
    public static Sprint CreateSprint(SprintTypes sprintType)
    {
        return sprintType switch
        {
            SprintTypes.Release => new ReleaseSprint(),
            SprintTypes.Review => new ReviewSprint(),
            _ => throw new InvalidOperationException("Invalid sprint type.")
        };
    }
}