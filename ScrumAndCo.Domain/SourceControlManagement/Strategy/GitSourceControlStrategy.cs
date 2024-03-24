namespace ScrumAndCo.Domain.SourceControlManagement.Strategy;

public class GitSourceControlStrategy : ISourceControl
{
    private Dictionary<string, List<string>> _commitHistory = new Dictionary<string, List<string>>();

    public void CloneRepository(string repositoryUrl)
    {
        Console.WriteLine($"Cloning repository from {repositoryUrl}");
    }

    public void Commit(string message)
    {
        Console.WriteLine($"Committing changes with message: {message}");
        if (!_commitHistory.ContainsKey("master"))
        {
            _commitHistory["master"] = new List<string>();
        }
        _commitHistory["master"].Add(message);
    }

    public void Push()
    {
        Console.WriteLine("Pushing changes to remote repository");
    }

    public void Pull(string branchName)
    {
        Console.WriteLine($"Pulling changes from branch {branchName}");
    }

    public void Log(string branchName, bool includeLocal)
    {
        Console.WriteLine($"Commit history for branch {branchName}:");
        if (_commitHistory.ContainsKey(branchName))
        {
            foreach (var commit in _commitHistory[branchName])
            {
                Console.WriteLine(commit);
            }
        }
        else
        {
            Console.WriteLine("No commit history available for this branch.");
        }
    }

    public void CheckoutBranch(string branchName)
    {
        Console.WriteLine($"Switching to branch {branchName}");
    }

    public void MergeBranch(string fromBranch, string intoBranch)
    {
        Console.WriteLine($"Merging changes from branch {fromBranch} into branch {intoBranch}");
    }
}