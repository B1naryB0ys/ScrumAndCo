using ScrumAndCo.Domain.BacklogItems;
using ScrumAndCo.Domain.Sprints;

namespace ScrumAndCo.Domain;

public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public List<Sprint> Sprints { get; set; }
    public List<Enrollment> Members { get; set; }
    public List<BacklogItem> BacklogItems { get; set; }
    public List<Forum.Thread> Forum { get; set; }
    
    public Project(string name, string description, string imageUrl, User user)
    {
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        
        // Navigation properties
        Members = new List<Enrollment>()
        {
            new Enrollment(user, ProjectRole.SCRUM_MASTER)
        };
        Sprints = new List<Sprint>();
        BacklogItems = new List<BacklogItem>();
        Forum = new List<Forum.Thread>();
    }
    
    public void AddBacklogItem(BacklogItem item)
    {
        BacklogItems.Add(item);
    }
    
    public void AddSprint(Sprint sprint)
    {
        Sprints.Add(sprint);
    }
    
    public void AddThreadToForum(Forum.Thread thread)
    {
        Forum.Add(thread);
    }
    
    public void AddMember(User user, ProjectRole role)
    {
        switch (role)
        {
            case ProjectRole.SCRUM_MASTER when Members.Any(e => e.Role == ProjectRole.SCRUM_MASTER):
                throw new InvalidOperationException("A project can only have one Scrum Master");
            case ProjectRole.PRODUCT_OWNER when Members.Any(e => e.Role == ProjectRole.PRODUCT_OWNER):
                throw new InvalidOperationException("A project can only have one Product Owner");
            case ProjectRole.LEAD_DEVELOPER when Members.Any(e => e.Role == ProjectRole.LEAD_DEVELOPER):
                throw new InvalidOperationException("A project can only have one Lead developer");
            default:
                Members.Add(new Enrollment(user, role));
                break;
        }
    }
    
    public List<User?> GetProjectTesters()
    {
        return Members.Where(e => e.Role == ProjectRole.TESTER).Select(e => e.User).ToList();
    }
    
    public User GetProjectScrumMaster()
    {
        return Members.Where(e => e.Role == ProjectRole.SCRUM_MASTER).Select(e => e.User).First() ?? throw new InvalidOperationException();
    }
    
    public User GetProductOwner()
    {
        return Members.Where(e => e.Role == ProjectRole.PRODUCT_OWNER).Select(e => e.User).First() ?? throw new InvalidOperationException();
    }
    
    public bool CanUserMoveBacklogItemToDone(User user)
    {
        return Members.Any(e => e.User == user && e.Role == ProjectRole.DEVELOPER || e.Role == ProjectRole.DEVELOPER);
    }
}