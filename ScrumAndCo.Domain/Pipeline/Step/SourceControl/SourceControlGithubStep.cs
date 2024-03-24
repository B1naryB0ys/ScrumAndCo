namespace ScrumAndCo.Domain.Pipeline.Step.Source;

public class SourceControlGithubStep : PipelineStep
{
    public override void AcceptVisitor(IPipelineVisitor visitor)
    {
        visitor.Visit(this);
    }
}