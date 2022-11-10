using GameLog.Domain.Common.Exceptions;

namespace GameLog.Domain.Common;

public record FullName
{
    public string FirstName { get; }
    public string LastName { get; }

    public FullName(string firstName, string lastName)
    {
        if (string.IsNullOrEmpty(firstName))
            throw new InvalidParameterException("First name cannot be null nor empty", nameof(firstName));
        
        if (string.IsNullOrEmpty(lastName))
            throw new InvalidParameterException("Last name cannot be null nor empty", nameof(lastName));
        
        FirstName = firstName;
        LastName = lastName;
    }
}