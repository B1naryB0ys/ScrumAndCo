namespace ScrumAndCo.Domain.Pipeline.Step.Package;

public class PackageStep : PipelineStep
{ 
    public override void AcceptVisitor(IPipelineVisitor visitor)
    {
        visitor.Visit(this);
    }
}