using Moq;
using ScrumAndCo.Domain;
using ScrumAndCo.Domain.BacklogItems;
using ScrumAndCo.Domain.BacklogItems.States;
using ScrumAndCo.Domain.Exceptions;
using ScrumAndCo.Domain.Notifications;
using ScrumAndCo.Domain.Pipeline;
using ScrumAndCo.Domain.Sprints;
using Task = ScrumAndCo.Domain.Task;

namespace ScrumAndCo.Test;

public class BacklogItemTests
{
    // FR-3.1 During development, i want to be able to change the state of the backlog item in the correct order
    [Fact]
    public void Test_Changing_State_In_Correct_Order_Should_Change_State_Accordingly()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));

        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        var backlogItem = new BacklogItem("Test Backlog Item", "Test Description", project,
            new NotificationSubject<string>());

        var leadDeveloper = new User("Jane", "Doe", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        project.AddMember(leadDeveloper, ProjectRole.LEAD_DEVELOPER);

        // Act
        backlogItem.ToInProgressState();
        backlogItem.ToReadyForTestingState();
        backlogItem.ToTestingState();
        backlogItem.ToTestedState();
        backlogItem.ToDoneState(leadDeveloper);

        // Assert
        Assert.IsType<DoneState>(backlogItem._backlogItemState);
    }
    
    // FR-3.1 During development, i should not be able to change the state of the backlog item in the wrong order
    [Fact]
    public void Test_Changing_State_In_Incorrect_Order_Should_Throw_Exception()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));

        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        var backlogItem = new BacklogItem("Test Backlog Item", "Test Description", project,
            new NotificationSubject<string>());

        var leadDeveloper = new User("Jane", "Doe", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        project.AddMember(leadDeveloper, ProjectRole.LEAD_DEVELOPER);

        // Act
        backlogItem.ToInProgressState();
        backlogItem.ToReadyForTestingState();

        // Assert
        Assert.Throws<IllegalStateException>(() => backlogItem.ToTestedState());
    }

    // FR-3.2 As a tester, i want to be notified when a new backlog item is ready for testing
    [Fact]
    public void Test_ReadyForTestingState_Should_Notify_Testers()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        var tester = new User("Jane", "Doe", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        project.AddMember(tester, ProjectRole.TESTER);
        
        var notificationPreferenceService = new Mock<INotificationService>();
        notificationPreferenceService.Setup(x => x.SendMessage(It.IsAny<string>()));
        var backlogItem = new BacklogItem("Test Backlog Item", "Test Description", project, new NotificationSubject<string>());
        
        // Act
        tester.AddNotificationPreference(notificationPreferenceService.Object);
        backlogItem.ToInProgressState();
        backlogItem.ToReadyForTestingState();
        
        // Assert
        notificationPreferenceService.Verify(x => x.SendMessage(It.IsAny<string>()), Times.Once);
    }
    
    // FR-3.3 As a tester, i want to be able to move an item back to the TodoState from the TestingState
    [Fact]
    public void Test_Moving_Back_To_TodoState_Should_Be_Possible()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        var tester = new User("Jane", "Doe", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        project.AddMember(tester, ProjectRole.TESTER);

        // Act
        var backlogItem = new BacklogItem("Test Backlog Item", "Test Description", project, new NotificationSubject<string>());
        backlogItem.ToInProgressState();
        backlogItem.ToReadyForTestingState();
        backlogItem.ToTestingState();
        backlogItem.ToTodoState();
        
        // Assert
        Assert.IsType<TodoState>(backlogItem._backlogItemState);
    }
    
    // FR-3.4 As a scrum master, i want to receive a notification when a item is moved back to the TodoState from the TestingState
    [Fact]
    public void Test_Moving_Back_To_TodoState_Should_Notify_ScrumMaster()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        var backlogItem = new BacklogItem("Test Backlog Item", "Test Description", project,
            new NotificationSubject<string>());
        var tester = new User("Jane", "Doe", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        project.AddMember(tester, ProjectRole.TESTER);
        
        var notificationPreferenceService = new Mock<INotificationService>();
        notificationPreferenceService.Setup(x => x.SendMessage(It.IsAny<string>()));
        scrumMaster.AddNotificationPreference(notificationPreferenceService.Object);
        
        // Act

        backlogItem.ToInProgressState();
        backlogItem.ToReadyForTestingState();
        backlogItem.ToTestingState();
        backlogItem.ToTodoState();

        // Assert
        notificationPreferenceService.Verify(x => x.SendMessage(It.IsAny<string>()), Times.Once);
    }
    
    // FR-3.5 As a lead developer, i want to be able to move an item to the DoneState from the TestedState
    [Fact]
    public void Test_Moving_To_DoneState_From_TestedState_Should_Be_Possible_As_A_Lead_Developer()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        var backlogItem = new BacklogItem("Test Backlog Item", "Test Description", project,
            new NotificationSubject<string>());
        var leadDeveloper = new User("Jane", "Doe", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        project.AddMember(leadDeveloper, ProjectRole.LEAD_DEVELOPER);
        
        // Act
        backlogItem.ToInProgressState();
        backlogItem.ToReadyForTestingState();
        backlogItem.ToTestingState();
        backlogItem.ToTestedState();
        backlogItem.ToDoneState(leadDeveloper);
        
        // Assert
        Assert.IsType<DoneState>(backlogItem._backlogItemState);
    }

    // FR-3.5 As a developer, is should not be able to move an item to the DoneState from the TestedState
    [Fact]
    public void Test_Moving_To_DoneState_From_TestedState_Should_Not_Be_Possible_As_A_Developer()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        var backlogItem = new BacklogItem("Test Backlog Item", "Test Description", project,
            new NotificationSubject<string>());
        var leadDeveloper = new User("Jane", "Doe", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        project.AddMember(leadDeveloper, ProjectRole.LEAD_DEVELOPER);
        
        // Act
        backlogItem.ToInProgressState();
        backlogItem.ToReadyForTestingState();
        backlogItem.ToTestingState();
        backlogItem.ToTestedState();
        backlogItem.ToDoneState(leadDeveloper);
        
        // Assert
        Assert.IsType<DoneState>(backlogItem._backlogItemState);
    }
    
    // 3.6 As a lead developer, i want to be able to move an item back to the ReadyForTesting from the TestedState
    [Fact]
    public void Test_Moving_Back_To_ReadyForTestingState_From_TestedState_Should_Be_Possible_As_A_Lead_Developer()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));

        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        var backlogItem = new BacklogItem("Test Backlog Item", "Test Description", project,
            new NotificationSubject<string>());
        var leadDeveloper = new User("Jane", "Doe", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        project.AddMember(leadDeveloper, ProjectRole.LEAD_DEVELOPER);

        // Act
        backlogItem.ToInProgressState();
        backlogItem.ToReadyForTestingState();
        backlogItem.ToTestingState();
        backlogItem.ToTestedState();
        backlogItem.ToDoneState(leadDeveloper);

        // Assert
        Assert.IsType<DoneState>(backlogItem._backlogItemState);
    }
    
    // FR-6.1 During development, i want to be able to add tasks to a backlog item
    [Fact]
    public void Test_Adding_Tasks_To_BacklogItem_Should_Be_Possible()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));

        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        var backlogItem = new BacklogItem("Test Backlog Item", "Test Description", project,
            new NotificationSubject<string>());
        
        var developer = new User("Jane", "Doe", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        project.AddMember(developer, ProjectRole.DEVELOPER);
        
        // Act
        backlogItem.ToInProgressState();
        backlogItem.AddTask(new Task("Test Task", "Test Description", developer));

        // Assert
        Assert.Single(backlogItem.Tasks);
    }
    
    // FR-6.2 A backlogItem can only be marked as done when all tasks are completed
    [Fact]
    public void Test_Marking_BacklogItem_As_Done_When_All_Tasks_Completed_Should_Be_Possible()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));

        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        var backlogItem = new BacklogItem("Test Backlog Item", "Test Description", project,
            new NotificationSubject<string>());
        
        var leadDeveloper = new User("Jane", "Doe", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        project.AddMember(leadDeveloper, ProjectRole.LEAD_DEVELOPER);
        var task = new Task("Test Task", "Test Description", leadDeveloper);
        backlogItem.AddTask(task);
        
        // Act
        backlogItem.ToInProgressState();
        task.MarkAsDone();
        backlogItem.ToReadyForTestingState();
        backlogItem.ToTestingState();
        backlogItem.ToTestedState();
        backlogItem.ToDoneState(leadDeveloper);

        // Assert
        Assert.Single(backlogItem.Tasks);
    }
    
    // FR-6.2 Moving a backlogItem to the DoneState when not all tasks are completed should throw an exception
    [Fact]
    public void Test_Marking_BacklogItem_As_Done_When_Not_All_Tasks_Completed_Should_Throw_Exception()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));

        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        var backlogItem = new BacklogItem("Test Backlog Item", "Test Description", project,
            new NotificationSubject<string>());
        
        var leadDeveloper = new User("Jane", "Doe", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        project.AddMember(leadDeveloper, ProjectRole.LEAD_DEVELOPER);
        var task = new Task("Test Task", "Test Description", leadDeveloper);
        backlogItem.AddTask(task);
        
        // Act
        backlogItem.ToInProgressState();
        backlogItem.ToReadyForTestingState();
        backlogItem.ToTestingState();
        backlogItem.ToTestedState();

        // Assert
        Assert.Throws<IllegalStateActionException>(() => backlogItem.ToDoneState(leadDeveloper));
    }
    
    // FR-7.1 During development, 1 user should be assigned per backlog item
    [Fact]
    public void Test_Assigning_User_To_BacklogItem_Should_Be_Possible()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));

        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        var backlogItem = new BacklogItem("Test Backlog Item", "Test Description", project,
            new NotificationSubject<string>());
            
        var developer = new User("Jane", "Doe", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        project.AddMember(developer, ProjectRole.DEVELOPER);
            
        // Act
        backlogItem.AssignTask(developer);

        // Assert
        Assert.NotNull(backlogItem.AssignedTo);
    }
    
    // FR-7.2 During development, When a user is assigned to an already assigned backlog item, the scrum master should be notified
    [Fact]
    public void Test_Assigning_User_To_Already_Assigned_BacklogItem_Should_Notify_ScrumMaster()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var developer = new User("Jane", "Doe", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        var developerTwo = new User("Jarrod", "Doe", "jarrod.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        project.AddMember(developer, ProjectRole.DEVELOPER);
        project.AddMember(developerTwo, ProjectRole.DEVELOPER);
        var backlogItem = new BacklogItem("Test Backlog Item", "Test Description", project,
            new NotificationSubject<string>());
        backlogItem.AssignTask(developer);
        
        var notificationPreferenceService = new Mock<INotificationService>();
        notificationPreferenceService.Setup(x => x.SendMessage(It.IsAny<string>()));
        scrumMaster.AddNotificationPreference(notificationPreferenceService.Object);
        
        // Act
        backlogItem.AssignTask(developerTwo); // Item is already assigned to developer

        // Assert
        notificationPreferenceService.Verify(x => x.SendMessage(It.IsAny<string>()), Times.Once);
    }
    
    
}