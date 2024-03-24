namespace ScrumAndCo.Domain.Pipeline.Step.Analysis;

public class SonarCubeCompositeStep : PipelineComposite
{
    public override void AcceptVisitor(IPipelineVisitor visitor)
    {
        visitor.Visit(this);
        base.AcceptVisitor(visitor);
    }
}