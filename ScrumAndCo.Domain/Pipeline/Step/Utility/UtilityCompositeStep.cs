namespace ScrumAndCo.Domain.Pipeline.Step.Utility;

public class UtilityCompositeStep : PipelineComposite
{
    public override void AcceptVisitor(IPipelineVisitor visitor)
    {
        visitor.Visit(this);
        base.AcceptVisitor(visitor);
    }
}