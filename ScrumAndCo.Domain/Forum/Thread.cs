using ScrumAndCo.Domain.BacklogItems;
using ScrumAndCo.Domain.Forum.States;
using ScrumAndCo.Domain.Notifications;
using ThreadState = ScrumAndCo.Domain.Forum.States.ThreadState;

namespace ScrumAndCo.Domain.Forum;

public class Thread
{
    public ThreadState _threadState;
    public ISubject<string> _notificationSubject;
    public string Topic;
    public string Description;
    public BacklogItem BacklogItem;
    public User Author;
    public List<Comment> Comments;
    
    
    public Thread(string topic, string description, BacklogItem backlogItem, User author, ISubject<string> notificationSubject)
    {
        this._threadState = new OpenState(this);
        this._notificationSubject = notificationSubject;
        this.Topic = topic;
        this.Description = description;
        this.BacklogItem = backlogItem;
        this.Author = author;
        this.Comments = new List<Comment>();
        
        // Attach all project members to the notification subject
        foreach (var projectMember in backlogItem.Project.Members)
        {
            _notificationSubject.Attach(projectMember.User);
        }
    }
    
    public void NextThreadState()
    {
        _threadState.NextThreadState();
    }

    public void AddComment(Comment comment)
    {
        _threadState.AddComment(comment);
    }
    
    public void ChangeThreadState(ThreadState threadState)
    {
        Console.WriteLine($"New thread state: {threadState.GetType().FullName}");
        _threadState = threadState;
    }
}