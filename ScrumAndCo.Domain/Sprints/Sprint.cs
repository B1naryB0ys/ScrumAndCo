using ScrumAndCo.Domain.BacklogItems;
using ScrumAndCo.Domain.Sprints.States;

namespace ScrumAndCo.Domain.Sprints;

public abstract class Sprint
{
    private SprintState _sprintState;
    
    // Internal properties can only be set from the constructor and in the PlanningState
    internal string Name { get; set; }
    internal string Description { get; set; }
    internal DateOnly ActiveFrom { get; set; }
    internal DateOnly ActiveUntil { get; set; }
    
    public Project Project { get; set; }
    public List<BacklogItem> BacklogItems { get; set; }
    
    public Sprint(string name, string description, DateOnly activeFrom, DateOnly activeUntil, Project project)
    {
        Name = name;
        Description = description;
        ActiveFrom = activeFrom;
        ActiveUntil = activeUntil;
        Project = project;
        
        BacklogItems = new List<BacklogItem>();
        _sprintState = new PlanningState(this);
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
    
    // Method to change the properties of the sprint (Can only be called from the PlanningState)
    public void ChangeProperties(string name, string description, DateOnly activeFrom, DateOnly activeUntil)
    {
        _sprintState.ChangeProperties(name, description, activeFrom, activeUntil);
    }
    
    // Method to add a backlog item to the sprint (Can only be called from the PlanningState)
    public void AddBacklogItem(BacklogItem backlogItem)
    {
        _sprintState.AddBacklogItem(backlogItem);
    }
    
    // Internal method (only accessible from the sprint state namespace) to change the sprint state to the given state
    internal void ChangeSprintState(SprintState sprintState)
    {
        Console.WriteLine($"New sprint state: {sprintState.GetType().FullName}");
        _sprintState = sprintState;
    }
    
    // Visitor pattern method to accept a sprint visitor
    internal abstract void AcceptVisitor(ISprintVisitor visitor);
}