using ScrumAndCo.Domain.Sprint.State;

namespace ScrumAndCo.Domain.Sprint;

public class Sprint
{
    private SprintState _sprintState;
    
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly ActiveFrom { get; set; }
    public DateOnly ActiveUntil { get; set; }
    
    
    public Sprint()
    {
        this._sprintState = new PlanningState(this);
    }

    public void NextSprintState()
    {
        _sprintState.NextSprintState();
    }
    
    public void ChangeSprintState(SprintState sprintState)
    {
        Console.WriteLine($"New sprint state: {sprintState.GetType().FullName}");
        _sprintState = sprintState;
    }
}