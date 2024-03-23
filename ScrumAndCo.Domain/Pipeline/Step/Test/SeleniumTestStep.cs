namespace ScrumAndCo.Domain.Pipeline.Step.Test;

public class SeleniumTestStep : PipelineStep
{
    public override void AcceptVisitor(IPipelineVisitor visitor)
    {
        visitor.Visit(this);
    }
}