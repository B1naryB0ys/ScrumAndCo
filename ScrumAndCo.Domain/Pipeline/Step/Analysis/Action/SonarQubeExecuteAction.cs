namespace ScrumAndCo.Domain.Pipeline.Step.Analysis.Action;

public class SonarQubeExecuteAction : PipelineStep
{
    public override void AcceptVisitor(IPipelineVisitor visitor)
    {
        visitor.Visit(this);
    }
}