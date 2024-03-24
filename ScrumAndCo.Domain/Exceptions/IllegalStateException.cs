namespace ScrumAndCo.Domain.Exceptions;

[Serializable]
public class IllegalStateException : Exception
{
    public IllegalStateException()
    {
    }

    public IllegalStateException(string message)
        : base(message)
    {
    }
}