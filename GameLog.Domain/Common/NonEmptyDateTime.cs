using GameLog.Domain.Common.Exceptions;

namespace GameLog.Domain.Common;

public record NonEmptyDateTime : IComparable<NonEmptyDateTime>
{
    public DateTimeOffset Value { get; }

    public NonEmptyDateTime(DateTimeOffset value)
    {
        if (value <= DateTimeOffset.UnixEpoch)
            throw new InvalidParameterException("Created At date must be after the UNIX epoch", nameof(value));
        
        Value = value;
    }
    
    public int CompareTo(NonEmptyDateTime? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return Value.CompareTo(other.Value);
    }
}