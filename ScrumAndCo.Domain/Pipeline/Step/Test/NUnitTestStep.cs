namespace ScrumAndCo.Domain.Pipeline.Step.Test;

public class NUnitTestStep : PipelineStep
{
    public override void AcceptVisitor(IPipelineVisitor visitor)
    {
        visitor.Visit(this);
    }
}