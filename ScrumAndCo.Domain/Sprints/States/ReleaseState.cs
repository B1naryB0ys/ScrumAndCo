using ScrumAndCo.Domain.Exceptions;
using ScrumAndCo.Domain.Notifications;
using ScrumAndCo.Domain.Pipeline;

namespace ScrumAndCo.Domain.Sprints.States;

public class ReleaseState : SprintState
{
    public ReleaseState(Sprint context) : base(context)
    {
         _context.NotificationSubject.Attach(_context.Project.GetProjectScrumMaster());
         _context.NotificationSubject.Attach(_context.Project.GetProductOwner());
    }

    // Used for cancelling the release state when the sprint is rejected by a member
    public override void NextSprintState()
    {
        _context.ChangeSprintState(new ClosedState(_context));
        
        // Notify the scrum master and product owner 
        _context.NotificationSubject.NotifyAll($"The sprint: {_context.Name} has been rejected and therefore cancelled");
    }

    public override void RunPipeLine(bool retry = true)
    {
        try
        {
            _context.Pipeline.AcceptVisitor(new PipelineVisitor());
            
            // Notify the scrum master and product owner when the pipeline succeeds
            _context.NotificationSubject.NotifyAll("The pipeline has successfully executed");
            
            NextSprintState();
        }
        catch (Exception ex)
        {
            // Notify only the scrum master when a pipeline fails
            _context.NotificationSubject.NotifySingle($"The pipeline failed with the error: {ex.Message}", _context.Project.GetProjectScrumMaster());

            if (!retry)
            {
                // Cancel the release state when the pipeline fails (back to finished state)
                _context.ChangeSprintState(new FinishedState(_context));
                return;
            }
            
            // Retry the pipeline when it fails (retry chosen by the user)
            RunPipeLine(false);
        }
    }
}
