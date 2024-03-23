using ScrumAndCo.Domain.BacklogItems.States;
using ScrumAndCo.Domain.Notifications;
using ScrumAndCo.Domain.Sprints;

namespace ScrumAndCo.Domain.BacklogItems;

public class BacklogItem
{
    private ItemState _backlogItemState;
    
    internal NotificationSubject<string> NotificationSubject = new NotificationSubject<string>();
    
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    public string Description { get; set; }
    
    public Project Project { get; set; }
    public Sprint? Sprint { get; set; }
    public User? AssignedTo { get; set; }
    public List<Task> Tasks { get; set; }
    
    public BacklogItem(string name, string description, Project project)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Project = project;
        _backlogItemState = new TodoState(this);
        Tasks = new List<Task>();
        
        // Add the scrum master as a subscriber to the notification subject
        NotificationSubject.Attach(Project.GetProjectScrumMaster());
    }
    
    public void Subscribe(User user)
    {
        NotificationSubject.Attach(user);
    }
    
    public void AddTask(Task task)
    {
        Tasks.Add(task);
    }
    
    public void RemoveTask(Task task)
    {
        Tasks.Remove(task);
    }
    
    public bool AllTasksCompleted()
    {
        return Tasks.All(t => t.IsDone);
    }
    
    public void AssignTask(User user)
    {
        if(AssignedTo != null)
        {
            Console.WriteLine($"Backlog item is already assigned to {AssignedTo.FirstName} {AssignedTo.LastName}");
            
            // Alerting the scrum master that the project members are trying to reassign the task
            NotificationSubject.NotifySingle($"{AssignedTo.FirstName} is trying to swap his task with {user.FirstName}",Project.GetProjectScrumMaster());
            
            return;
        }
        
        AssignedTo = user;
    }
    
    // State pattern methods
    internal void ToTodoState()
    {
        _backlogItemState = new TodoState(this);
    }

    internal void ToInProgressState()
    {
        _backlogItemState = new InProgressState(this);
    }

    internal void ToReadyForTestingState()
    {
        _backlogItemState = new ReadyForTestingState(this);
    }

    internal void ToTestingState()
    {
        _backlogItemState = new TestingState(this);
    }

    internal void ToTestedState()
    {
        _backlogItemState = new TestedState(this);
    }

    internal void ToDoneState()
    {
        _backlogItemState = new DoneState(this);
    }
}