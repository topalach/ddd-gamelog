using GameLog.Domain.Common.Exceptions;

namespace GameLog.Domain.Common;

public abstract record Id
{
    public string Value { get; }

    protected Id(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new InvalidParameterException("Gamer ID cannot be null nor empty", nameof(value));

        Value = value;
    }
}