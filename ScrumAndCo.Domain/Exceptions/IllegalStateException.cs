namespace ScrumAndCo.Domain.Exceptions;

[Serializable]
public class IllegalStateException(string message) : Exception(message);