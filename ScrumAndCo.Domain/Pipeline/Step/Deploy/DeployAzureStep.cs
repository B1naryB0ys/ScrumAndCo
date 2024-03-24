namespace ScrumAndCo.Domain.Pipeline.Step.Deploy;

public class DeployAzureStep : PipelineStep
{
    public override void AcceptVisitor(IPipelineVisitor visitor)
    {
        visitor.Visit(this);
    }
}