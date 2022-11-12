namespace GameLog.Domain.Common.Exceptions;

public class InvalidAggregateStateException : Exception
{
    public InvalidAggregateStateException(string message) : base(message)
    {
    }
}