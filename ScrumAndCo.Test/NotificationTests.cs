using ScrumAndCo.Domain;
using ScrumAndCo.Domain.BacklogItems;
using ScrumAndCo.Domain.Notifications;
using ScrumAndCo.Domain.Pipeline;
using ScrumAndCo.Domain.Sprints;

namespace ScrumAndCo.Test;

public class NotificationTests
{
    // // FR-8.1 As a user, i want to be able to receive notifications through email
    // [Fact]
    // public void User_Should_Receive_Notification_Through_Email()
    // {
    //     // Arrange
    //     var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
    //     var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
    //     var emailNotificationService = new EmailNotificationService();
    //     scrumMaster.AddNotificationPreference(emailNotificationService);
    //     
    //     var stringWriter = new StringWriter();
    //     Console.SetOut(stringWriter);
    //     
    //     // Act
    //     scrumMaster.Update("Test message");
    //     
    //     // Assert
    //     var actualMessage = stringWriter.ToString().Trim();
    //     Assert.Equal("Sending email notification: Test message", actualMessage);
    // }
    //
    // // FR-8.2 As a user, i want to be able to receive notifications through slack
    // [Fact]
    // public void User_Should_Receive_Notification_Through_Slack()
    // {
    //     // Arrange
    //     var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
    //     var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
    //     var slackNotificationService = new SlackNotificationService();
    //     scrumMaster.AddNotificationPreference(slackNotificationService);
    //     
    //     var stringWriter = new StringWriter();
    //     Console.SetOut(stringWriter);
    //     
    //     // Act
    //     scrumMaster.Update("Test message");
    //     
    //     // Assert
    //     var actualMessage = stringWriter.ToString().Trim();
    //     Assert.Equal("Sending slack notification: Test message", actualMessage);
    // }
}