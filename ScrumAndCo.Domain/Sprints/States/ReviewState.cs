using ScrumAndCo.Domain.Exceptions;

namespace ScrumAndCo.Domain.Sprints.States;

public class ReviewState : SprintState
{
    private string _documentUrl = string.Empty;
    
    public ReviewState(Sprint context) : base(context)
    {
    }
    
    public override void NextSprintState()
    {
        if (_documentUrl == string.Empty)
            throw new IllegalStateActionException("You must upload a review document before closing the sprint.");
        _context.ChangeSprintState(new ClosedState(_context));
    }

    public override void UploadReview(string documentUrl)
    {
        Console.WriteLine($"Review document uploaded: {documentUrl}");
        _documentUrl = documentUrl;
    }
}