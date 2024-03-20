using ScrumAndCo.Domain.Sprints.States;

namespace ScrumAndCo.Domain.Sprints;

public abstract class Sprint
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

    // Method to change the sprint state to the next state (f.e from PlanningState to OngoingState)
    public void NextSprintState()
    {
        _sprintState.NextSprintState();
    }

    // Method to cancel the sprint, this method will change the sprint state to CancelledState (finished state)
    public void CancelSprint()
    {
        ChangeSprintState(new CancelledState(this));
    }
    
    // Internal method (only accessible from the sprint state namespace) to change the sprint state to the given state
    internal void ChangeSprintState(SprintState sprintState)
    {
        Console.WriteLine($"New sprint state: {sprintState.GetType().FullName}");
        _sprintState = sprintState;
    }
}