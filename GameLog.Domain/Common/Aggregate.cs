using GameLog.Domain.Common.Exceptions;

namespace GameLog.Domain.Common;

public abstract class Aggregate<TId> where TId : Id
{
#pragma warning disable CS8618
    public TId Id { get; protected set; }
#pragma warning restore CS8618
}

public abstract class Aggregate<TId, TEvent> : Aggregate<TId>
    where TId : Id
    where TEvent : Event
{
    private readonly List<TEvent> _changes = new();

    protected void Apply(TEvent @event)
    {
        When(@event);
        EnsureValidState();
        _changes.Add(@event);
    }

    private void EnsureValidState()
    {
        var validationMessages = GetStateValidationErrors()
            .Select(x => x.ToErrorMessage())
            .ToList();
        
        if (validationMessages.Any() == false)
            return;
        
        var errorMessage = string.Join(Environment.NewLine, validationMessages);

        throw new InvalidAggregateStateException(errorMessage);
    }

    protected abstract void When(TEvent @event);

    protected virtual IEnumerable<InvalidStateError> GetStateValidationErrors() => Array.Empty<InvalidStateError>();

    protected class InvalidStateError
    {
        public string FieldName { get; }
        public string Message { get; }

        public InvalidStateError(string fieldName, string message)
        {
            FieldName = fieldName;
            Message = message;
        }

        public string ToErrorMessage() => $"[{FieldName}] {Message}";
    }
}