namespace ScrumAndCo.Domain;

public class Enrollment
{
    public DateTime JoinedOn { get; set; }
    public ProjectRole Role { get; set; }
    
    // Navigation properties
    public User? User { get; set; }
    
    public Enrollment(User user, ProjectRole role)
    {
        User = user;
        Role = role;
        JoinedOn = DateTime.Now;
    }
}