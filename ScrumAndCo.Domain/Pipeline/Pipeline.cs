namespace ScrumAndCo.Domain.Pipeline;

public class Pipeline : PipelineComposite
{
    public override void AcceptVisitor(IPipelineVisitor visitor)
    {
        visitor.Visit(this);
        base.AcceptVisitor(visitor);
    }
}