using GameLog.Common.Validation;
using GameLog.Domain.Common.Exceptions;

namespace GameLog.Domain.Common;

public record FullName
{
    private const int FirstNameMaxLength = ValidationConstants.MaxLength.FirstName;
    private const int LastNameMaxLength = ValidationConstants.MaxLength.LastName;
    
    public string FirstName { get; }
    public string LastName { get; }

    public FullName(string firstName, string lastName)
    {
        if (string.IsNullOrEmpty(firstName))
            throw new InvalidParameterException("First name cannot be null nor empty", nameof(firstName));
        
        if (string.IsNullOrEmpty(lastName))
            throw new InvalidParameterException("Last name cannot be null nor empty", nameof(lastName));

        if (firstName.Length > FirstNameMaxLength)
        {
            throw new InvalidParameterException(
                $"First name cannot be longer than {FirstNameMaxLength} characters",
                nameof(firstName));
        }

        if (lastName.Length > LastNameMaxLength)
        {
            throw new InvalidParameterException(
                $"Last name cannot be longer than {LastNameMaxLength} characters",
                nameof(lastName));
        }

        FirstName = firstName;
        LastName = lastName;
    }
}