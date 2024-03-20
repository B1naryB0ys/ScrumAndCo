using ScrumAndCo.Domain.Sprints;

namespace ScrumAndCo.Domain;

public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    
    public List<Enrollment> Members { get; set; }
    public List<Sprint> Sprints { get; set; }
    
    public Project(string name, string description, string imageUrl)
    {
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        
        Members = new List<Enrollment>();
        Sprints = new List<Sprint>();
    }
}