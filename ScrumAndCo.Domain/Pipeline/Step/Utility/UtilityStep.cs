namespace ScrumAndCo.Domain.Pipeline.Step.Utility;

public class UtilityStep : PipelineStep
{
    public override void AcceptVisitor(IPipelineVisitor visitor)
    {
        visitor.Visit(this);
    }
}