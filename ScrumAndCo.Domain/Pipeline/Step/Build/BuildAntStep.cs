namespace ScrumAndCo.Domain.Pipeline.Step.Build;

public class BuildAntStep : PipelineStep
{
    public override void AcceptVisitor(IPipelineVisitor visitor)
    {
        visitor.Visit(this);
    }
}