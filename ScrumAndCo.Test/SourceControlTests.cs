using ScrumAndCo.Domain.SourceControlManagement;
using ScrumAndCo.Domain.SourceControlManagement.Strategy;

namespace ScrumAndCo.Test;

public class SourceControlTests
{
    // FR-13.1 Creating a new branch should add it to the repository
    [Fact]
    public void Test_New_Branch_Is_Added_To_Repository()
    {
        // Arrange
        var strategy = new GitSourceControlStrategy();
        var sourceController = new SourceControl(strategy);

        // Act
        sourceController.CheckoutBranch("TestBranch");
        
        // Assert
        Assert.Equal("TestBranch", sourceController.GetCurrentBranch());
    }
    
    // FR-13.3 Checking out a branch should switch to that branch
    [Fact]
    public void Test_Checkout_Branch_Switches_To_Branch()
    {
        // Arrange
        var strategy = new GitSourceControlStrategy();
        var sourceController = new SourceControl(strategy);

        // Act
        sourceController.CheckoutBranch("TestBranch");
        
        // Assert
        Assert.Equal("TestBranch", sourceController.GetCurrentBranch());
    }
    
    // FR-13.4 A developer should be able to push changes to the remote repository
    [Fact]
    public void Test_Push_Changes_To_Remote_Repository()
    {
        // Arrange
        var strategy = new GitSourceControlStrategy();
        var sourceController = new SourceControl(strategy);

        // Act
        sourceController.CheckoutBranch("TestBranch");
        sourceController.Commit("Test commit message");
        sourceController.Push();
        
        // Assert
        Assert.Single(sourceController.GetCommitHistory("TestBranch"));
    }
    
    // FR-13.5 A developer should be able to commit changes to the current branch
    [Fact]
    public void Test_Commit_Changes_To_Current_Branch()
    {
        // Arrange
        var strategy = new GitSourceControlStrategy();
        var sourceController = new SourceControl(strategy);

        // Act
        sourceController.CheckoutBranch("TestBranch");
        sourceController.Commit("Test commit message");
        
        // Assert
        Assert.Single(sourceController.GetCommitHistory("TestBranch"));
    }
}