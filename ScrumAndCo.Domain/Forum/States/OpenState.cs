namespace ScrumAndCo.Domain.Forum.States;

public class OpenState(Thread context) : ThreadState(context)
{
    public override void NextThreadState()
    {
        _context.ChangeThreadState(new ClosedState(_context));
    }
}