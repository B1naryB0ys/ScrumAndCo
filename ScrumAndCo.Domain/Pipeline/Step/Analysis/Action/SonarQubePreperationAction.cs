namespace ScrumAndCo.Domain.Pipeline.Step.Analysis.Action;

public class SonarQubePreperationAction : PipelineStep
{
    public override void AcceptVisitor(IPipelineVisitor visitor)
    {
        visitor.Visit(this);
    }
}