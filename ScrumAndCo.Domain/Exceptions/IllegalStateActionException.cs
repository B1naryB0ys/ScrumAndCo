namespace ScrumAndCo.Domain.Exceptions;

[Serializable]
public class IllegalStateActionException(string message) : Exception(message);