using GameLog.Domain.Exceptions;

namespace GameLog.Domain.Common;

public record CreatedAt
{
    public DateTimeOffset Value { get; }

    public CreatedAt(DateTimeOffset value)
    {
        if (value <= DateTimeOffset.UnixEpoch)
            throw new InvalidParameterException("Created At date must be later than UNIX epoch", nameof(value));
        
        Value = value;
    }
}