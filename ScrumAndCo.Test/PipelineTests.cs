using Moq;
using ScrumAndCo.Domain;
using ScrumAndCo.Domain.Exceptions;
using ScrumAndCo.Domain.Notifications;
using ScrumAndCo.Domain.Pipeline;
using ScrumAndCo.Domain.Pipeline.Step.Analysis;
using ScrumAndCo.Domain.Pipeline.Step.Analysis.Action;
using ScrumAndCo.Domain.Pipeline.Step.Build;
using ScrumAndCo.Domain.Pipeline.Step.Deploy;
using ScrumAndCo.Domain.Pipeline.Step.Package;
using ScrumAndCo.Domain.Pipeline.Step.Source;
using ScrumAndCo.Domain.Pipeline.Step.SourceControl;
using ScrumAndCo.Domain.Pipeline.Step.Test;
using ScrumAndCo.Domain.Pipeline.Step.Utility;
using ScrumAndCo.Domain.Sprints;

namespace ScrumAndCo.Test;

public class PipelineTests
{
    // FR-4.6 A pipeline can be run when a sprint is in release state
    [Fact]
    public void Test_Pipeline_Can_Run_When_Sprint_Is_In_Release_State()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var productOwner = new User("Jane", "the product owner", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        project.CreateSprint("Test Sprint", "A description for the test sprint", DateOnly.Parse("2024-03-24"),
            DateOnly.Parse("2024-03-30"), new Pipeline(), new NotificationSubject<string>(), SprintType.Release);
        project.AddMember(productOwner, ProjectRole.PRODUCT_OWNER);
        var sprint = (ReleaseSprint)project.Sprints[0];
        
        var pipeline = new Pipeline();
        pipeline.AddPipelineComponent(new BuildAntStep());
        pipeline.AddPipelineComponent(new DeployAzureStep());
        sprint.SetPipeline(pipeline);
        
        // Act
        sprint.NextSprintState(); // To Starting state (OngoingState)
        sprint.NextSprintState(); // To Finishing state (FinishedState)
        sprint.NextSprintState(); // To Review state (ReviewState)
        
        // Assert
        var exception = Record.Exception(() => sprint.RunPipeline());
        Assert.Null(exception);
    }
    
    // FR-4.7 A sprint is locked when the pipeline is running (no changes can be made)
    [Fact]
    public void Test_Sprint_Is_Locked_When_Pipeline_Is_Running()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var productOwner = new User("Jane", "the product owner", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        project.CreateSprint("Test Sprint", "A description for the test sprint", DateOnly.Parse("2024-03-24"),
            DateOnly.Parse("2024-03-30"), new Pipeline(), new NotificationSubject<string>(), SprintType.Release);
        project.AddMember(productOwner, ProjectRole.PRODUCT_OWNER);
        var sprint = (ReleaseSprint)project.Sprints[0];
        
        var pipeline = new Pipeline();
        pipeline.AddPipelineComponent(new BuildAntStep());
        pipeline.AddPipelineComponent(new DeployAzureStep());
        sprint.SetPipeline(pipeline);
        
        // Act
        sprint.NextSprintState(); // To Starting state (OngoingState)
        sprint.NextSprintState(); // To Finishing state (FinishedState)
        sprint.NextSprintState(); // To Review state (ReviewState)
        sprint.RunPipeline();
        
        // Assert
        Assert.Throws<IllegalStateActionException>(sprint.NextSprintState);
    }
    
    // FR-4.8 A notification is sent to the scrum master and product owner when the pipeline succeeds
    [Fact]
    public void Test_Notification_Is_Sent_To_Scrum_Master_And_Product_Owner_When_Pipeline_Succeeds()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var productOwner = new User("Jane", "the product owner", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        project.CreateSprint("Test Sprint", "A description for the test sprint", DateOnly.Parse("2024-03-24"),
            DateOnly.Parse("2024-03-30"), new Pipeline(), new NotificationSubject<string>(), SprintType.Release);
        project.AddMember(productOwner, ProjectRole.PRODUCT_OWNER);
        var sprint = (ReleaseSprint)project.Sprints[0];
        
        var pipeline = new Pipeline();
        pipeline.AddPipelineComponent(new BuildAntStep());
        pipeline.AddPipelineComponent(new DeployAzureStep());
        sprint.SetPipeline(pipeline);

        var notificationPreferenceService = new Mock<INotificationService>();
        notificationPreferenceService.Setup(x => x.SendMessage(It.IsAny<string>()));
        scrumMaster.AddNotificationPreference(notificationPreferenceService.Object);
        productOwner.AddNotificationPreference(notificationPreferenceService.Object);
        
        // Act
        sprint.NextSprintState(); // To Starting state (OngoingState)
        sprint.NextSprintState(); // To Finishing state (FinishedState)
        sprint.NextSprintState(); // To Review state (ReviewState)
        sprint.RunPipeline();
        
        // Assert
        notificationPreferenceService.Verify(x => x.SendMessage(It.IsAny<string>()), Times.AtLeast(2));
    }
    
    // FR-12.1 A pipeline can execute source steps
    [Fact]
    public void Test_Pipeline_Can_Execute_Source_Steps()
    {
        // Arrange
        var pipeline = new Pipeline();
        pipeline.AddPipelineComponent(new SourceControlAzureStep());
        
        var pipelineVisitorMock = new Mock<IPipelineVisitor>();
        
        // Act
        pipeline.AcceptVisitor(pipelineVisitorMock.Object);
        
        // Assert
        pipelineVisitorMock.Verify(x => x.Visit(It.IsAny<BuildAntStep>()), Times.Once);
    }
    
    // FR-12.2 A pipeline can execute package steps
    [Fact]
    public void Test_Pipeline_Can_Execute_Package_Steps()
    {
        // Arrange
        var pipeline = new Pipeline();
        pipeline.AddPipelineComponent(new SourceControlGithubStep());
        pipeline.AddPipelineComponent(new PackageStep());
        
        var pipelineVisitorMock = new Mock<IPipelineVisitor>();
        
        // Act
        pipeline.AcceptVisitor(pipelineVisitorMock.Object);
        
        // Assert
        pipelineVisitorMock.Verify(x => x.Visit(It.IsAny<PackageStep>()), Times.Once);
    }
    
    // FR-12.3 A pipeline can execute build steps
    [Fact]
    public void Test_Pipeline_Can_Execute_Build_Steps()
    {
        // Arrange
        var pipeline = new Pipeline();
        pipeline.AddPipelineComponent(new SeleniumTestStep());
        pipeline.AddPipelineComponent(new BuildAntStep());
        
        var pipelineVisitorMock = new Mock<IPipelineVisitor>();
        
        // Act
        pipeline.AcceptVisitor(pipelineVisitorMock.Object);
        
        // Assert
        pipelineVisitorMock.Verify(x => x.Visit(It.IsAny<BuildAntStep>()), Times.Once);
    }
    
    // FR-12.4 A pipeline can execute test steps
    [Fact]
    public void Test_Pipeline_Can_Execute_Test_Steps()
    {
        // Arrange
        var pipeline = new Pipeline();
        pipeline.AddPipelineComponent(new NUnitTestStep());
        
        var pipelineVisitorMock = new Mock<IPipelineVisitor>();
        
        // Act
        pipeline.AcceptVisitor(pipelineVisitorMock.Object);
        
        // Assert
        pipelineVisitorMock.Verify(x => x.Visit(It.IsAny<NUnitTestStep>()), Times.Once);
    }
    
    // FR-12.5 A pipeline can execute multiple analysis steps
    [Fact]
    public void Test_Pipeline_Can_Execute_Multiple_Analysis_Steps()
    {
        // Arrange
        var pipeline = new Pipeline();
        var analysisCompositeStep = new SonarCubeCompositeStep();
        analysisCompositeStep.AddPipelineComponent(new SonarQubePreperationAction());
        analysisCompositeStep.AddPipelineComponent(new SonarQubeExecuteAction());
        analysisCompositeStep.AddPipelineComponent(new SonarQubeReportingAction());
        pipeline.AddPipelineComponent(analysisCompositeStep);
        
        var pipelineVisitorMock = new Mock<IPipelineVisitor>();
        
        // Act
        pipeline.AcceptVisitor(pipelineVisitorMock.Object);
        
        // Assert
        pipelineVisitorMock.Verify(x => x.Visit(It.IsAny<SonarQubePreperationAction>()), Times.Once);
        pipelineVisitorMock.Verify(x => x.Visit(It.IsAny<SonarQubeExecuteAction>()), Times.Once);
        pipelineVisitorMock.Verify(x => x.Visit(It.IsAny<SonarQubeReportingAction>()), Times.Once);
    }
    
    // FR-12.6 A pipeline can execute deploy steps
    [Fact]
    public void Test_Pipeline_Can_Execute_Deploy_Steps()
    {
        // Arrange
        var pipeline = new Pipeline();
        pipeline.AddPipelineComponent(new DeployAzureStep());
        
        var pipelineVisitorMock = new Mock<IPipelineVisitor>();
        
        // Act
        pipeline.AcceptVisitor(pipelineVisitorMock.Object);
        
        // Assert
        pipelineVisitorMock.Verify(x => x.Visit(It.IsAny<DeployAzureStep>()), Times.Once);
    }
    
    // FR-12.7 A pipeline can execute multiple utility steps
    [Fact]
    public void Test_Pipeline_Can_Execute_Multiple_Utility_Steps()
    {
        // Arrange
        var pipeline = new Pipeline();
        var utilityCompositeStep = new UtilityCompositeStep();
        utilityCompositeStep.AddPipelineComponent(new UtilityStep());
        utilityCompositeStep.AddPipelineComponent(new UtilityStep());
        pipeline.AddPipelineComponent(utilityCompositeStep);
        
        var pipelineVisitorMock = new Mock<IPipelineVisitor>();
        
        // Act
        pipeline.AcceptVisitor(pipelineVisitorMock.Object);
        
        // Assert
        pipelineVisitorMock.Verify(x => x.Visit(It.IsAny<UtilityStep>()), Times.Exactly(2));
    }
    
    // FR-12.8 A pipeline can execute all steps
    [Fact]
    public void Test_Pipeline_Can_Execute_All_Steps()
    {
        // Arrange
        var pipeline = new Pipeline();
        pipeline.AddPipelineComponent(new SourceControlAzureStep());
        pipeline.AddPipelineComponent(new PackageStep());
        pipeline.AddPipelineComponent(new BuildAntStep());
        pipeline.AddPipelineComponent(new NUnitTestStep());
        var analysisCompositeStep = new SonarCubeCompositeStep();
        analysisCompositeStep.AddPipelineComponent(new SonarQubePreperationAction());
        analysisCompositeStep.AddPipelineComponent(new SonarQubeExecuteAction());
        analysisCompositeStep.AddPipelineComponent(new SonarQubeReportingAction());
        pipeline.AddPipelineComponent(analysisCompositeStep);
        pipeline.AddPipelineComponent(new DeployAzureStep());
        var utilityCompositeStep = new UtilityCompositeStep();
        utilityCompositeStep.AddPipelineComponent(new UtilityStep());
        utilityCompositeStep.AddPipelineComponent(new UtilityStep());
        pipeline.AddPipelineComponent(utilityCompositeStep);
        
        var pipelineVisitorMock = new Mock<IPipelineVisitor>();
        
        // Act
        pipeline.AcceptVisitor(pipelineVisitorMock.Object);
        
        // Assert
        pipelineVisitorMock.Verify(x => x.Visit(It.IsAny<SourceControlAzureStep>()), Times.Once);
        pipelineVisitorMock.Verify(x => x.Visit(It.IsAny<PackageStep>()), Times.Once);
        pipelineVisitorMock.Verify(x => x.Visit(It.IsAny<BuildAntStep>()), Times.Once);
        pipelineVisitorMock.Verify(x => x.Visit(It.IsAny<NUnitTestStep>()), Times.Once);
        pipelineVisitorMock.Verify(x => x.Visit(It.IsAny<SonarQubePreperationAction>()), Times.Once);
        pipelineVisitorMock.Verify(x => x.Visit(It.IsAny<SonarQubeExecuteAction>()), Times.Once);
        pipelineVisitorMock.Verify(x => x.Visit(It.IsAny<SonarQubeReportingAction>()), Times.Once);
        pipelineVisitorMock.Verify(x => x.Visit(It.IsAny<DeployAzureStep>()), Times.Once);
        pipelineVisitorMock.Verify(x => x.Visit(It.IsAny<UtilityStep>()), Times.Exactly(2));
    }
}