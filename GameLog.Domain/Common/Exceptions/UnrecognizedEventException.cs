namespace GameLog.Domain.Common.Exceptions;

public class UnrecognizedEventException : Exception
{
    public UnrecognizedEventException(Event @event) : base($"Cannot recognize event {@event.Name()}")
    {
    }
}