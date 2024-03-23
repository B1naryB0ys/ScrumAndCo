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

public interface IPipelineVisitor
{   
    public void Visit(Pipeline pipeline);
    
    // Build steps
    public void Visit(BuildAntStep step);
    
    // Deployment steps
    public void Visit(DeployAzureStep step);
    
    // Package steps
    public void Visit(PackageStep step);
    
    // Utility steps
    public void Visit(UtilityCompositeStep step);
    public void Visit(UtilityStep step);
    
    // Test steps
    public void Visit(NUnitTestStep step);
    
    public void Visit(SeleniumTestStep step);
    
    // Source control steps
    public void Visit(SourceControlGithubStep step);
    public void Visit(SourceControlAzureStep step);
    
    // Analysis steps
    public void Visit(SonarCubeCompositeStep step);
    
    // Analysis actions
    public void Visit(SonarQubeExecuteAction action);
    public void Visit(SonarQubePreperationAction action);
    public void Visit(SonarQubeReportingAction action);
}
