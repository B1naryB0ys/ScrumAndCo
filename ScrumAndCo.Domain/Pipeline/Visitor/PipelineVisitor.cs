

using ScrumAndCo.Domain.Pipeline.Step.Analysis;
using ScrumAndCo.Domain.Pipeline.Step.Analysis.Action;
using ScrumAndCo.Domain.Pipeline.Step.Build;
using ScrumAndCo.Domain.Pipeline.Step.Deploy;
using ScrumAndCo.Domain.Pipeline.Step.Package;
using ScrumAndCo.Domain.Pipeline.Step.Source;
using ScrumAndCo.Domain.Pipeline.Step.SourceControl;
using ScrumAndCo.Domain.Pipeline.Step.Test;
using ScrumAndCo.Domain.Pipeline.Step.Utility;

namespace ScrumAndCo.Domain.Pipeline;

public class PipelineVisitor : IPipelineVisitor
{
    public void Visit(Pipeline pipeline)
    {
        pipeline.Execute();
    }

    public void Visit(BuildAntStep step)
    {
        step.Execute();
    }

    public void Visit(DeployAzureStep step)
    {
        step.Execute();
    }

    public void Visit(PackageStep step)
    {
        step.Execute();
    }

    public void Visit(UtilityCompositeStep step)
    {
        step.Execute();
    }

    public void Visit(UtilityStep step)
    {
        step.Execute();
    }

    public void Visit(NUnitTestStep step)
    {
        step.Execute();
    }

    public void Visit(SeleniumTestStep step)
    {
        step.Execute();
    }

    public void Visit(SourceControlGithubStep step)
    {
        step.Execute();
    }

    public void Visit(SourceControlAzureStep step)
    {
        step.Execute();
    }

    public void Visit(SonarCubeCompositeStep step)
    {
        step.Execute();
    }

    public void Visit(SonarQubeExecuteAction action)
    {
        action.Execute();
    }

    public void Visit(SonarQubePreperationAction action)
    {
        action.Execute();
    }

    public void Visit(SonarQubeReportingAction action)
    {
        action.Execute();
    }
}