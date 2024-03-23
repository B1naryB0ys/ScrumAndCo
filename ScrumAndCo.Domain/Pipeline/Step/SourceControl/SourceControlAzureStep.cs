namespace ScrumAndCo.Domain.Pipeline.Step.SourceControl
{
    public class SourceControlAzureStep : PipelineStep
    {
        public override void AcceptVisitor(IPipelineVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

