using GameLog.Domain.Exceptions;

namespace GameLog.Domain.Common;

public record Name
{
    public string Value { get; }

    public Name(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new InvalidParameterException("Name cannot be null nor empty", nameof(value));
        
        Value = value;
    }
}