using ScrumAndCo.Domain;
using ScrumAndCo.Domain.Sprints;

namespace ScrumAndCo.Test;

public class ProjectTests
{
    [Fact]
    // FR-1 As a scrum master, i want to be able to create a project
    public void Creating_Project_Should_Be_Created_As_A_Scrum_Master()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));

        // Act
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());

        // Assert
        Assert.NotNull(project);
        Assert.Equal("ScrumAndCo Testing", project.Name);
        Assert.Equal(ProjectRole.SCRUM_MASTER, project.Members[0].Role);
    }

    [Fact]
    // FR-2 As a project member, i want to be able to add additional members to the project
    public void Adding_Member_To_Project_Should_Be_Possible()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        
        // Act
        var member = new User("Jane", "Doe", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        project.AddMember(member, ProjectRole.DEVELOPER);
    }

    [Fact]
    // FR-10.1 A project should only have one scrum master
    public void Adding_More_Than_One_Scrum_Master_Should_Throw_Exception()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());
        var additionalScrumMaster = new User("Jane", "Doe", "jane.doe@mail.com", new DateOnly(1980, 1, 1));

        // Act / Assert
        Assert.Throws<InvalidOperationException>(() => project.AddMember(additionalScrumMaster, ProjectRole.SCRUM_MASTER));
    }

    [Fact]
    // FR-10.2 A project should only have one product owner
    public void Adding_More_Than_One_Product_Owner_Should_Throw_Exception()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());

        var productOwner = new User("Jane", "Doe", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        project.AddMember(productOwner, ProjectRole.PRODUCT_OWNER);

        var additionalProductOwner = new User("Jarrod", "Doe", "jarrod.doe@mail.com", new DateOnly(1980, 1, 1));

        // Act / Assert
        Assert.Throws<InvalidOperationException>(() => project.AddMember(additionalProductOwner, ProjectRole.PRODUCT_OWNER));
    }

    [Fact]
    // FR-10.3 A project should only have one lead developer
    public void Adding_More_Than_One_Lead_Developer_Should_Throw_Exception()
    {
        // Arrange
        var scrumMaster = new User("John", "the master of scrum", "john.doe@mail.com", new DateOnly(1980, 1, 1));
        var project = new Project("ScrumAndCo Testing", "", "https://scrumandco.com", scrumMaster, new SprintFactory());

        var leadDeveloper = new User("Jane", "Doe", "jane.doe@mail.com", new DateOnly(1980, 1, 1));
        project.AddMember(leadDeveloper, ProjectRole.LEAD_DEVELOPER);

        var additionalLeadDeveloper = new User("Jarrod", "Doe", "jarrod.doe@mail.com", new DateOnly(1980, 1, 1));

        // Act / Assert
        Assert.Throws<InvalidOperationException>(() =>
            project.AddMember(additionalLeadDeveloper, ProjectRole.LEAD_DEVELOPER));
    }
}