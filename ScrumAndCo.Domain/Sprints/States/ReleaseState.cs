using ScrumAndCo.Domain.Exceptions;
using ScrumAndCo.Domain.Notifications;
using ScrumAndCo.Domain.Pipeline;

namespace ScrumAndCo.Domain.Sprints.States;

public class ReleaseState : SprintState
{
    private readonly NotificationSubject<string> _notificationSubject = new();
    
    public ReleaseState(Sprint context) : base(context)
    {
        // TODO: Add pipeline fail notification receiver
        _notificationSubject.Attach(_context.Project.GetProjectScrumMaster());
        _notificationSubject.Attach(_context.Project.GetProductOwner());
        
        RunPipeline();
    }

    public override void NextSprintState()
    {
        _context.ChangeSprintState(new ClosedState(_context));
    }

    private void RunPipeline(bool retry = true)
    {
        try
        {
            _context.Pipeline.AcceptVisitor(new PipelineVisitor());
            
            // Notify the scrum master and product owner when the pipeline succeeds
            _notificationSubject.NotifyAll("The pipeline has successfully executed");
            
            NextSprintState();
        }
        catch (Exception ex)
        {
            // Notify only the scrum master when a pipeline fails
            _notificationSubject.NotifySingle($"The pipeline failed with the error: {ex.Message}", _context.Project.GetProjectScrumMaster());

            if (!retry)
            {
                // Cancel the release state when the pipeline fails (back to finished state)
                _context.ChangeSprintState(new FinishedState(_context));
                return;
            }
            
            // Retry the pipeline when it fails (retry chosen by the user)
            RunPipeline(false);
        }
    }
}
