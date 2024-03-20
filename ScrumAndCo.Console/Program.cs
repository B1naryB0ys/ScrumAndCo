// See https://aka.ms/new-console-template for more information

using ScrumAndCo.Domain.BacklogItems;
using ScrumAndCo.Domain.BacklogItems.States;
using ScrumAndCo.Domain.Sprints;

// Testing sprint states
/*var sprint = new Sprint();
sprint.NextSprintState();
sprint.CancelSprint();
sprint.NextSprintState();
sprint.NextSprintState();
sprint.NextSprintState();*/

var backlogItem = new BacklogItem("Aanmaken button", "Aanmaken van een nieuwe button");
backlogItem.ChangeItemState(new TodoState(backlogItem));
backlogItem.ChangeItemState(new TestingState(backlogItem));

SprintFactory.CreateSprint(SprintTypes.Release);
SprintFactory.CreateSprint(SprintTypes.Review);