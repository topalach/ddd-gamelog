using GameLog.Domain.Exceptions;

namespace GameLog.Domain.Common;

public record Nickname
{
    private const int MaxLength = ValidationConstants.MaxLength.Nickname;
    
    public string Value { get; }
    
    public Nickname(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new InvalidParameterException("Nickname cannot be null nor empty", nameof(value));

        if (value.Length > MaxLength)
        {
            throw new InvalidParameterException($"Nickname cannot be longer than {MaxLength} characters", nameof(value));
        }

        Value = value;
    }
}