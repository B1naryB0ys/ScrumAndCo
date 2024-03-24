using Moq;
using ScrumAndCo.Domain;
using ScrumAndCo.Domain.BacklogItems;
using ScrumAndCo.Domain.BacklogItems.States;
using ScrumAndCo.Domain.Exceptions;
using ScrumAndCo.Domain.Forum.States;
using ScrumAndCo.Domain.Notifications;
using ScrumAndCo.Domain.Pipeline;
using ScrumAndCo.Domain.Sprints;
using ClosedState = ScrumAndCo.Domain.Sprints.States.ClosedState;

namespace ScrumAndCo.Test;

public class SprintTests
{
    // FR-4.1 During development, I want to be able to create a review sprint (FR-4.3) for a project
    [Fact]
    public void Creating_Review_Sprint_Should_Be_Possible()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var sprintFactoryMock = new Mock<ISprintFactory>();
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, sprintFactoryMock.Object);
        sprintFactoryMock.Setup(x => x.CreateSprint(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateOnly>(), It.IsAny<DateOnly>(), It.IsAny<Project>(), It.IsAny<Pipeline>(), It.IsAny<ISubject<string>>(),It.IsAny<SprintType>()))
            .Returns(new ReviewSprint("Test Sprint", "A description for the test sprint", DateOnly.Parse("2024-03-24"), DateOnly.Parse("2024-03-30"), project, new Pipeline(), new NotificationSubject<string>() ));
        
        // Act
        project.CreateSprint("Test Sprint", "A description for the test sprint", DateOnly.Parse("2024-03-24"), DateOnly.Parse("2024-03-30"), new Pipeline(), new NotificationSubject<string>(), SprintType.Review);
        
        // Assert
        Assert.IsType<ReviewSprint>(project.Sprints[0]);
        Assert.Single(project.Sprints);
        sprintFactoryMock.Verify(x => x.CreateSprint( It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateOnly>(), It.IsAny<DateOnly>(), It.IsAny<Project>(), It.IsAny<Pipeline>(), It.IsAny<ISubject<string>>(), It.IsAny<SprintType>()), Times.Once);
    }
    
    // FR-4.1 During development, I want to be able to create a release sprint (FR-4.3) for a project
    [Fact]
    public void Creating_Release_Sprint_Should_Be_Possible()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var sprintFactoryMock = new Mock<ISprintFactory>();
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, sprintFactoryMock.Object);
        sprintFactoryMock.Setup(x => x.CreateSprint(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateOnly>(), It.IsAny<DateOnly>(), It.IsAny<Project>(), It.IsAny<Pipeline>(),  It.IsAny<ISubject<string>>(),It.IsAny<SprintType>()))
            .Returns(new ReleaseSprint("Test Sprint", "A description for the test sprint", DateOnly.Parse("2024-03-24"), DateOnly.Parse("2024-03-30"), project, new Pipeline(), new NotificationSubject<string>()));
        
        // Act
        project.CreateSprint("Test Sprint", "A description for the test sprint", DateOnly.Parse("2024-03-24"), DateOnly.Parse("2024-03-30"), new Pipeline(), new NotificationSubject<string>(), SprintType.Release);
        
        // Assert
        Assert.IsType<ReleaseSprint>(project.Sprints[0]);
        Assert.Single(project.Sprints);
        sprintFactoryMock.Verify(x => x.CreateSprint( It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateOnly>(), It.IsAny<DateOnly>(), It.IsAny<Project>(), It.IsAny<Pipeline>(), It.IsAny<ISubject<string>>(), It.IsAny<SprintType>()), Times.Once);
    }
    
    // FR-4.2 When a sprint has started, no details should be able to be changed (name, description, start date, end date)
    [Fact]
    public void Changing_Sprint_Details_Should_Throw_Exception_When_Changing()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        project.CreateSprint("Test Sprint", "A description for the test sprint", DateOnly.Parse("2024-03-24"), DateOnly.Parse("2024-03-30"), new Pipeline(), new NotificationSubject<string>(), SprintType.Release);
        var sprint = project.Sprints[0];
        // Act
        sprint.NextSprintState(); // Starting sprint
        
        // Assert
        Assert.Throws<IllegalStateActionException>(() => sprint.ChangeProperties("New sprint name", "New sprint description", DateOnly.Parse("2024-03-10"), DateOnly.Parse("2024-03-20")));
    }
    
    // FR-4.2 When a sprint has started, no details should be able to be changed (name, description, start date, end date)
    [Fact]
    public void Changing_Sprint_Details_Should_Be_Possible_When_In_Planning_State()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        project.CreateSprint("Test Sprint", "A description for the test sprint", DateOnly.Parse("2024-03-24"),
            DateOnly.Parse("2024-03-30"), new Pipeline(), new NotificationSubject<string>(), SprintType.Release);
        var sprint = project.Sprints[0];

        // Act
        sprint.ChangeProperties("New sprint name", "New sprint description", DateOnly.Parse("2024-03-10"),
            DateOnly.Parse("2024-03-20"));

        // Assert
        Assert.Equal("New sprint name", sprint.Name);
        Assert.Equal("New sprint description", sprint.Description);
        Assert.Equal(DateOnly.Parse("2024-03-10"), sprint.ActiveFrom);
        Assert.Equal(DateOnly.Parse("2024-03-20"), sprint.ActiveUntil);
    }
    
    // FR-4.4 When a ReviewSprint is finished, a review document should be uploaded before the sprint can be closed
    [Fact]
    public void Closing_Review_Sprint_With_Review_Document_Should_Change_Sprint_State()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        project.CreateSprint("Test Sprint", "A description for the test sprint", DateOnly.Parse("2024-03-24"),
            DateOnly.Parse("2024-03-30"), new Pipeline(), new NotificationSubject<string>(), SprintType.Review);
        var sprint = (ReviewSprint)project.Sprints[0];

        // Act
        sprint.NextSprintState(); // To Starting state (OngoingState)
        sprint.NextSprintState(); // To Finishing state (FinishedState)
        sprint.NextSprintState(); // To Review state (ReviewState)
        sprint.AddReviewFileUrl("https://scrumandco.com/review.pdf");

        
        // Assert
        var exception = Record.Exception(() => sprint.NextSprintState());
        Assert.Null(exception);
    }
    
    // FR-4.4 When a ReviewSprint is finished, a sprint cannot be closed when there is no review document
    [Fact]
    public void Closing_Review_Sprint_Without_Review_Document_Should_Throw_Exception()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        project.CreateSprint("Test Sprint", "A description for the test sprint", DateOnly.Parse("2024-03-24"),
            DateOnly.Parse("2024-03-30"), new Pipeline(), new NotificationSubject<string>(), SprintType.Review);
        var sprint = (ReviewSprint)project.Sprints[0];

        // Act
        sprint.NextSprintState(); // To Starting state (OngoingState)
        sprint.NextSprintState(); // To Finishing state (FinishedState)
        sprint.NextSprintState(); // To Release state (ReviewState)

        
        // Assert
        Assert.Throws<IllegalStateActionException>(sprint.NextSprintState);
    }
    
    // FR-4.5 When a ReleaseSprint is rejected, the sprint should be cancelled
    [Fact]
    public void Rejecting_Release_Sprint_Should_Cancel_Sprint_And_Notify_Project()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        project.CreateSprint("Test Sprint", "A description for the test sprint", DateOnly.Parse("2024-03-24"),
            DateOnly.Parse("2024-03-30"), new Pipeline(), new NotificationSubject<string>(), SprintType.Release);
        var sprint = project.Sprints[0];
        
        var productOwner = new User("Jane", "Doe", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        project.AddMember(productOwner, ProjectRole.PRODUCT_OWNER);
        
        // Act
        sprint.NextSprintState(); // To Starting state (OngoingState)
        sprint.NextSprintState(); // To Finishing state (FinishedState)
        sprint.NextSprintState(); // To Release state (ReviewState)
        
        sprint.NextSprintState(); // This rejects and closes the sprint (ClosedState)
        
        // Assert
        Assert.IsType<ClosedState>(sprint._sprintState);
    }
    
    
    // FR-4.5 When a ReleaseSprint is rejected, the scrum master and product owner should be notified
    [Fact]
    public void Rejecting_Release_Sprint_Should_Notify_ScrumMaster_And_ProductOwner()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var productOwner = new User("Jane", "Doe", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        project.CreateSprint("Test Sprint", "A description for the test sprint", DateOnly.Parse("2024-03-24"),
            DateOnly.Parse("2024-03-30"), new Pipeline(), new NotificationSubject<string>(), SprintType.Release);
        var sprint = project.Sprints[0];
        project.AddMember(productOwner, ProjectRole.PRODUCT_OWNER);
        
        var notificationPreferenceService = new Mock<INotificationService>();
        notificationPreferenceService.Setup(x => x.SendMessage(It.IsAny<string>()));
        scrumMaster.AddNotificationPreference(notificationPreferenceService.Object);
        productOwner.AddNotificationPreference(notificationPreferenceService.Object);
        
        // Act
        sprint.NextSprintState(); // To Starting state (OngoingState)
        sprint.NextSprintState(); // To Finishing state (FinishedState)
        sprint.NextSprintState(); // To Release state (ReviewState)
        
        sprint.NextSprintState(); // This rejects and closes the sprint (ClosedState)
        
        // Assert
        notificationPreferenceService.Verify(x => x.SendMessage(It.IsAny<string>()), Times.Exactly(2));
    }
    
    // FR-5 During development, i should be able to add a backlog item to a sprint
    [Fact]
    public void Adding_Backlog_Item_To_Sprint_Should_Be_Possible()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        project.CreateSprint("Test Sprint", "A description for the test sprint", DateOnly.Parse("2024-03-24"),
            DateOnly.Parse("2024-03-30"), new Pipeline(), new NotificationSubject<string>(), SprintType.Release);
        var sprint = project.Sprints[0];
        
        // Act
        sprint.AddBacklogItem(new BacklogItem("Test item", "A description for the test item", project, new NotificationSubject<string>()));
        
        // Assert
        Assert.Single(sprint.BacklogItems);
    }
}   