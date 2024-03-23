namespace ScrumAndCo.Domain.SourceControlManagement;

public class SourceControl : ISourceControl
{
    private ISourceControl _sourceControlStrategy;
    
    public SourceControl(ISourceControl sourceControlStrategy)
    {
        _sourceControlStrategy = sourceControlStrategy;
    }
    
    public void CloneRepository(string repositoryUrl)
    {
        _sourceControlStrategy.CloneRepository(repositoryUrl);
    }

    public void Commit(string message)
    {
        _sourceControlStrategy.Commit(message);
    }

    public void Push()
    {
        _sourceControlStrategy.Push();
    }

    public void Pull(string branchName)
    {
        _sourceControlStrategy.Pull(branchName);
    }

    public void Log(string branchName, bool includeLocal)
    {
        _sourceControlStrategy.Log(branchName, includeLocal);
    }

    public void CheckoutBranch(string branchName)
    {
        _sourceControlStrategy.CheckoutBranch(branchName);
    }

    public void MergeBranch(string fromBranch, string intoBranch)
    {
        _sourceControlStrategy.MergeBranch(fromBranch, intoBranch);
    }
}