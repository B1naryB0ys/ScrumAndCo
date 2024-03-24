using ScrumAndCo.Domain.BacklogItems;
using ScrumAndCo.Domain.Notifications;
using ScrumAndCo.Domain.Sprints.States;
using ScrumAndCo.Domain.Pipeline;
namespace ScrumAndCo.Domain.Sprints;

public abstract class Sprint
{
    public SprintState _sprintState;
    
    // properties can only be set from the constructor and in the PlanningState
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly ActiveFrom { get; set; }
    public DateOnly ActiveUntil { get; set; }
    
    public Project Project { get; set; }
    public List<BacklogItem> BacklogItems { get; set; }
    public Pipeline.Pipeline Pipeline { get; set; }
    
    public ISubject<string> NotificationSubject { get; set; }
    
    public Sprint(string name, string description, DateOnly activeFrom, DateOnly activeUntil, Project project, Pipeline.Pipeline pipeline, ISubject<string> notificationSubject)
    {
        Name = name;
        Description = description;
        ActiveFrom = activeFrom;
        ActiveUntil = activeUntil;
        Project = project;
        
        BacklogItems = new List<BacklogItem>();
        _sprintState = new PlanningState(this);
        NotificationSubject = notificationSubject;
    }

    // Method to change the sprint state to the next state (f.e from PlanningState to OngoingState)
    public void NextSprintState()
    {
        _sprintState.NextSprintState();
    }

    // Method to cancel the sprint, this method will change the sprint state to ClosedState (cancelled state)
    public void CancelSprint()
    {
        ChangeSprintState(new ClosedState(this));
    }
    
    // Method to change the properties of the sprint (Can only be called from the PlanningState)
    public void ChangeProperties(string name, string description, DateOnly activeFrom, DateOnly activeUntil)
    {
        _sprintState.ChangeProperties(name, description, activeFrom, activeUntil);
    }
    
    // Method to run the pipeline (Can only be called from the ReleaseState)
    public void RunPipeline()
    {
        _sprintState.RunPipeLine();
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