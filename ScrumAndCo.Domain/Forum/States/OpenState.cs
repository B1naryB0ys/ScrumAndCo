using ScrumAndCo.Domain.Notifications;

namespace ScrumAndCo.Domain.Forum.States;

public class OpenState : ThreadState
{
    public OpenState(Thread context) : base(context)
    {
    }
    
    public override void NextThreadState()
    {
        _context.ChangeThreadState(new ClosedState(_context));
    }
    
    public override void AddComment(Comment comment)
    {
        _context.Comments.Add(comment);
        
        // Notify project members
        _context._notificationSubject.NotifyAll($"A new comment has been added to the thread: {_context.Topic}");
    }
}