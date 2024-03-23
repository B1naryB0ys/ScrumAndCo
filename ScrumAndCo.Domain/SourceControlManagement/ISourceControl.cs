namespace ScrumAndCo.Domain.SourceControlManagement;

public interface ISourceControl
{
    public void CloneRepository(string repositoryUrl);
    public void Commit(string message);
    public void Push();
    public void Pull(string branchName);
    public void Log(string branchName, bool includeLocal);
    
    public void CheckoutBranch(string branchName);
    public void MergeBranch(string fromBranch, string intoBranch);
}