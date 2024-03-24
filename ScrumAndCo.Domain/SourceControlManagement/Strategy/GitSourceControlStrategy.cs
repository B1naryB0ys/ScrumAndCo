namespace ScrumAndCo.Domain.SourceControlManagement.Strategy;

public class GitSourceControlStrategy : ISourceControl
{
    private Dictionary<string, List<string>> _repository = new Dictionary<string, List<string>>();
    private string _currentBranch = "master";

    public void CloneRepository(string repositoryUrl)
    {
        Console.WriteLine($"Cloning repository from {repositoryUrl}");
    }

    public void Commit(string message)
    {
        Console.WriteLine($"Committing changes with message: {message}");
        if (!_repository.ContainsKey(_currentBranch))
        {
            _repository[_currentBranch] = new List<string>();
        }
        _repository[_currentBranch].Add(message);
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
        if (_repository.ContainsKey(branchName))
        {
            foreach (var commit in _repository[branchName])
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
        // Check if branch exists
        if (!_repository.ContainsKey(branchName))
        {
            _repository[branchName] = new List<string>();
        }
        
        _currentBranch = branchName;
        Console.WriteLine($"Switching to branch {branchName}");
    }

    public string GetCurrentBranch()
    {
        return _currentBranch;
    }

    public List<string> GetCommitHistory(string branchName)
    {
        return _repository.ContainsKey(branchName) ? _repository[branchName] : new List<string>();
    }

    public void MergeBranch(string fromBranch, string intoBranch)
    {
        Console.WriteLine($"Merging changes from branch {fromBranch} into branch {intoBranch}");
    }
}