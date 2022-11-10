using GameLog.Common.Validation;
using GameLog.Domain.Common.Exceptions;

namespace GameLog.Domain.Common;

public record Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (EmailValidator.IsValid(value) == false)
            throw new InvalidParameterException("Email is invalid", nameof(value));
        
        Value = value;
    }
}