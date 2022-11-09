namespace GameLog.Domain.Exceptions;

public class InvalidParameterException : DomainValidationException
{
    public InvalidParameterException(string message, string parameterName)
        : base($"Parameter '{parameterName}' validation failed: {message}")
    {
    }
}