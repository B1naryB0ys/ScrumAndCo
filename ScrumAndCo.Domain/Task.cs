namespace ScrumAndCo.Domain;

public class Task
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public User AssignedTo { get; init; }
    
    public Task(string name, string description, User assignedTo)
    {
        Name = name;
        Description = description;
        AssignedTo = assignedTo;
    }
    
    public void MarkAsDone()
    {
        IsDone = true;
    }
}