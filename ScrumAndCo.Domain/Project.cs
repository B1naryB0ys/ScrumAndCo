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
    
    public Project(string name, string description, string imageUrl)
    {
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        
        Members = new List<Enrollment>();
        Sprints = new List<Sprint>();
        BacklogItems = new List<BacklogItem>();
    }
    
    public void AddBacklogItem(BacklogItem item)
    {
        BacklogItems.Add(item);
    }
    
    public List<User?> GetProjectTesters()
    {
        return Members.Where(e => e.Role == ProjectRole.TESTER).Select(e => e.User).ToList();
    }
    
    public User GetProjectScrumMaster()
    {
        return Members.Where(e => e.Role == ProjectRole.SCRUM_MASTER).Select(e => e.User).First() ?? throw new InvalidOperationException();
    }
    
    public bool CanUserMoveBacklogItemToDone(User user)
    {
        return Members.Any(e => e.User == user && e.Role == ProjectRole.DEVELOPER || e.Role == ProjectRole.DEVELOPER);
    }
}