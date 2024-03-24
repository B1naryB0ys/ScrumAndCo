using ScrumAndCo.Domain.Notifications;

namespace ScrumAndCo.Domain.Forum.States;

public class OpenState : ThreadState
{
    private readonly NotificationSubject<string> _notificationSubject = new();
    
    public OpenState(Thread context) : base(context)
    {
        // Notify project members
        foreach (var member in _context.BacklogItem.Project.Members)
        {
            _notificationSubject.Attach(member.User);
        }
    }
    
    public override void NextThreadState()
    {
        _context.ChangeThreadState(new ClosedState(_context));
    }
    
    public override void AddComment(Comment comment)
    {
        _context.Comments.Add(comment);
        
        // Notify project members
        _notificationSubject.NotifyAll($"A new comment has been added to the thread: {_context.Topic}");
    }
}