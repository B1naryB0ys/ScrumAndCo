using Moq;
using ScrumAndCo.Domain;
using ScrumAndCo.Domain.BacklogItems;
using ScrumAndCo.Domain.Forum;
using ScrumAndCo.Domain.Forum.States;
using ScrumAndCo.Domain.Notifications;
using ScrumAndCo.Domain.Pipeline;
using ScrumAndCo.Domain.Sprints;

namespace ScrumAndCo.Test;

public class ForumTests
{
    // FR-10.1 A discussion thread can be started on a backlog item
    [Fact]
    public void Test_Discussion_Can_Be_Started_On_Backlog_Item()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        project.CreateSprint("Test Sprint", "A description for the test sprint", DateOnly.Parse("2024-03-24"),
            DateOnly.Parse("2024-03-30"), new Pipeline(), new NotificationSubject<string>(), SprintType.Release);
        
        var sprint = project.Sprints[0];
        var backlogItem = new BacklogItem("Test item", "A description for the test item", project,
            new NotificationSubject<string>());
        sprint.AddBacklogItem(backlogItem);
        
        // Act
        var thread = new ScrumAndCo.Domain.Forum.Thread("Test thread", "A description for the test thread", backlogItem,
            scrumMaster, new NotificationSubject<string>());
        backlogItem.AddThread(thread);
        
        // Assert
        Assert.Contains(thread, backlogItem.Threads);
    }
    
    // FR-10.2 If a backlogItem is marked as finished, all discussion threads is closed
    [Fact]
    public void Test_All_Discussion_Threads_Are_Archived_When_Backlog_Item_Is_Finished()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var leadDeveloper = new User("Jane", "the lead developer", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        project.AddMember(leadDeveloper, ProjectRole.LEAD_DEVELOPER);
        project.CreateSprint("Test Sprint", "A description for the test sprint", DateOnly.Parse("2024-03-24"),
            DateOnly.Parse("2024-03-30"), new Pipeline(), new NotificationSubject<string>(), SprintType.Release);
        
        var sprint = project.Sprints[0];
        var backlogItem = new BacklogItem("Test item", "A description for the test item", project,
            new NotificationSubject<string>());
        sprint.AddBacklogItem(backlogItem);
        
        var thread = new ScrumAndCo.Domain.Forum.Thread("Test thread", "A description for the test thread", backlogItem,
            scrumMaster, new NotificationSubject<string>());
        backlogItem.AddThread(thread);
        
        // Act
        backlogItem.ToInProgressState();
        backlogItem.ToReadyForTestingState();
        backlogItem.ToTestingState();
        backlogItem.ToTestedState();
        backlogItem.ToDoneState(leadDeveloper);
        
        // Assert
        Assert.IsType<ArchivedState>(thread._threadState);
    }
    
    // FR-10.3 When a thread receives a comment, all members of the project are notified
    [Fact]
    public void Test_Members_Are_Notified_When_Thread_Receives_Comment()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var leadDeveloper = new User("Jane", "the lead developer", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        project.AddMember(leadDeveloper, ProjectRole.LEAD_DEVELOPER);
        project.CreateSprint("Test Sprint", "A description for the test sprint", DateOnly.Parse("2024-03-24"),
            DateOnly.Parse("2024-03-30"), new Pipeline(), new NotificationSubject<string>(), SprintType.Release);
            
        var sprint = project.Sprints[0];
        var backlogItem = new BacklogItem("Test item", "A description for the test item", project,
            new NotificationSubject<string>());
        sprint.AddBacklogItem(backlogItem);
            
        var thread = new ScrumAndCo.Domain.Forum.Thread("Test thread", "A description for the test thread", backlogItem,
            scrumMaster, new NotificationSubject<string>());
        backlogItem.AddThread(thread);
        
        var notificationPreferenceService = new Mock<INotificationService>();
        notificationPreferenceService.Setup(x => x.SendMessage(It.IsAny<string>()));
        scrumMaster.AddNotificationPreference(notificationPreferenceService.Object);
        leadDeveloper.AddNotificationPreference(notificationPreferenceService.Object);
            
        // Act
        thread.AddComment(new Comment("Test comment", leadDeveloper));
            
        // Assert
        notificationPreferenceService.Verify(x => x.SendMessage(It.IsAny<string>()), Times.Exactly(2));
    }
    
    // FR-10.4 When a thread is closed, it is moved to the project
    [Fact]
    public void Test_Thread_Is_Moved_To_Project_When_Closed()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var leadDeveloper = new User("Jane", "the lead developer", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        project.AddMember(leadDeveloper, ProjectRole.LEAD_DEVELOPER);
        project.CreateSprint("Test Sprint", "A description for the test sprint", DateOnly.Parse("2024-03-24"),
            DateOnly.Parse("2024-03-30"), new Pipeline(), new NotificationSubject<string>(), SprintType.Release);
            
        var sprint = project.Sprints[0];
        var backlogItem = new BacklogItem("Test item", "A description for the test item", project,
            new NotificationSubject<string>());
        sprint.AddBacklogItem(backlogItem);
            
        var thread = new ScrumAndCo.Domain.Forum.Thread("Test thread", "A description for the test thread", backlogItem,
            scrumMaster, new NotificationSubject<string>());
        backlogItem.AddThread(thread);
            
        // Act
        thread.NextThreadState();
            
        // Assert
        Assert.Contains(thread, project.Forum);
        Assert.DoesNotContain(thread, backlogItem.Threads);
    }
    
    // FR-10.4 When a thread is closed, it is moved to the project
    [Fact]
    public void Test_Thread_Is_Moved_To_Project_When_Archived()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var leadDeveloper = new User("Jane", "the lead developer", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        project.AddMember(leadDeveloper, ProjectRole.LEAD_DEVELOPER);
        project.CreateSprint("Test Sprint", "A description for the test sprint", DateOnly.Parse("2024-03-24"),
            DateOnly.Parse("2024-03-30"), new Pipeline(), new NotificationSubject<string>(), SprintType.Release);
        
        var sprint = project.Sprints[0];
        var backlogItem = new BacklogItem("Test item", "A description for the test item", project,
            new NotificationSubject<string>());
        sprint.AddBacklogItem(backlogItem);
        
        var thread = new ScrumAndCo.Domain.Forum.Thread("Test thread", "A description for the test thread", backlogItem,
            scrumMaster, new NotificationSubject<string>());
        backlogItem.AddThread(thread);
        
        // Act
        thread.ChangeThreadState(new ArchivedState(thread));
        
        // Assert
        Assert.Contains(thread, project.Forum);
        Assert.DoesNotContain(thread, backlogItem.Threads);
    }
}