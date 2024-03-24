using Moq;
using ScrumAndCo.Domain;
using ScrumAndCo.Domain.BacklogItems;
using ScrumAndCo.Domain.Notifications;

namespace ScrumAndCo.Test;

public class BacklogItemTests
{
    // FR-3.1 As a tester, i want to be notified when a new backlog item is ready for testing
    [Fact]
    public void Test_ReadyForTestingState_Should_Notify_Testers()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster);
        var tester = new User("Jane", "Doe", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        project.AddMember(tester, ProjectRole.TESTER);
        
        var notificationPreferenceService = new Mock<INotificationService>();
        notificationPreferenceService.Setup(x => x.SendMessage(It.IsAny<string>()));
        var backlogItem = new BacklogItem("Test Backlog Item", "Test Description", project, new NotificationSubject<string>());
        
        // Act
        tester.AddNotificationPreference(notificationPreferenceService.Object);
        backlogItem.ToReadyForTestingState();
        
        // Assert
        notificationPreferenceService.Verify(x => x.SendMessage(It.IsAny<string>()), Times.Once);
    }
}