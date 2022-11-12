﻿using GameLog.Domain.Common.Exceptions;

namespace GameLog.Domain.Common;

public abstract class Aggregate<TEvent> where TEvent : Event
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