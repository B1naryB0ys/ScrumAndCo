﻿using ScrumAndCo.Domain.BacklogItems.States;
using ScrumAndCo.Domain.Notifications;
using ScrumAndCo.Domain.Sprints;

namespace ScrumAndCo.Domain.BacklogItems;

public class BacklogItem
{
    public ItemState _backlogItemState;

    internal ISubject<string> _notificationSubject;
    
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    public string Description { get; set; }
    
    public Project Project { get; set; }
    public Sprint? Sprint { get; set; }
    public User? AssignedTo { get; set; }
    public List<Task> Tasks { get; set; }
    
    public List<Forum.Thread> Threads { get; set; }
    
    public BacklogItem(string name, string description, Project project, ISubject<string> notificationSubject)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Project = project;
        _backlogItemState = new TodoState(this);
        Tasks = new List<Task>();
        Threads = new List<Forum.Thread>();
        
        // Add the scrum master as a subscriber to the notification subject
        _notificationSubject = notificationSubject;
    }
    
    public void Subscribe(User user)
    {
        _notificationSubject.Attach(user);
    }
    
    public void AddTask(Task task)
    {
        Tasks.Add(task);
    }
    
    public void RemoveTask(Task task)
    {
        Tasks.Remove(task);
    }
    
    public void AddThread(Forum.Thread thread)
    {
        Threads.Add(thread);
    }
    
    public void RemoveThread(Forum.Thread thread)
    {
        Threads.Remove(thread);
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
            _notificationSubject.NotifySingle($"{AssignedTo.FirstName} is trying to swap his task with {user.FirstName}",Project.GetProjectScrumMaster());
            
            return;
        }
        
        AssignedTo = user;
    }
    
    // State pattern methods
    public void ToTodoState()
    {
        _backlogItemState.ToTodo();
    }

    public void ToInProgressState()
    {
        _backlogItemState.ToInProgress();
    }

    public void ToReadyForTestingState()
    {
        _backlogItemState.ToReadyForTesting();
    }

    public void ToTestingState()
    {
        _backlogItemState.ToTesting();
    }

    public void ToTestedState()
    {
        _backlogItemState.ToTested();
    }

    public void ToDoneState(User user)
    {
        _backlogItemState.ToDone(user);
    }
    
    internal void ChangeState(ItemState state)
    {
        _backlogItemState = state;
    }
}