namespace ScrumAndCo.Domain.Exceptions;

[Serializable]
public class IllegalStateActionException : Exception
{
    public IllegalStateActionException()
    {
    }

    public IllegalStateActionException(string message)
        : base(message)
    {
    }
}