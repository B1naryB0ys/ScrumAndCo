namespace ScrumAndCo.Domain.SourceControlManagement.Strategy;

public class SubversionStrategy : ISourceControl
{
    public void CloneRepository(string repositoryUrl)
    {
        throw new NotImplementedException();
    }

    public void Commit(string message)
    {
        throw new NotImplementedException();
    }

    public void Push()
    {
        throw new NotImplementedException();
    }

    public void Pull(string branchName)
    {
        throw new NotImplementedException();
    }

    public void Log(string branchName, bool includeLocal)
    {
        throw new NotImplementedException();
    }

    public void CheckoutBranch(string branchName)
    {
        throw new NotImplementedException();
    }

    public void MergeBranch(string fromBranch, string intoBranch)
    {
        throw new NotImplementedException();
    }
}