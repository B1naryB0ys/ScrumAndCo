namespace ScrumAndCo.Domain.Forum;

public class Comment
{
    public string Message;
    public User Author;

    public Comment(string message, User author)
    {
        this.Message = message;
        this.Author = author;
    }
}