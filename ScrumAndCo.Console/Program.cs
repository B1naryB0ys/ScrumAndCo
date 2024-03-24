// See https://aka.ms/new-console-template for more information

using ScrumAndCo.Domain;
using ScrumAndCo.Domain.BacklogItems;
using ScrumAndCo.Domain.BacklogItems.States;
using ScrumAndCo.Domain.Notifications;
using ScrumAndCo.Domain.Pipeline;
using ScrumAndCo.Domain.Pipeline.Step.Build;
using ScrumAndCo.Domain.Sprints;


// Arrange
var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster);
var tester = new User("Jane", "Doe", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
project.AddMember(tester, ProjectRole.TESTER);
var backlogItem = new BacklogItem("Test Backlog Item", "Test Description", project, new NotificationSubject<string>());

// Act
tester.AddNotificationPreference(new SlackNotificationService());
backlogItem.ToReadyForTestingState();
