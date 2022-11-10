using GameLog.Common.Validation;
using GameLog.Domain.Common;
using GameLog.Domain.Common.Exceptions;

namespace GameLog.Domain.GameProfiles;

public class GameProfile
{
    public GameProfileId Id { get; }
    public GameName Name { get; }
    public DevelopmentInfo DevelopmentInfo { get; }
    public NonEmptyDateTime CreatedAt { get; }

    public GameProfile(GameProfileId id, GameName name, DevelopmentInfo developmentInfo, NonEmptyDateTime createdAt)
    {
        Id = id;
        Name = name;
        DevelopmentInfo = developmentInfo;
        CreatedAt = createdAt;
    }
}

public record GameProfileId : Id
{
    public GameProfileId(string value) : base(value)
    {
    }
}

public record GameName
{
    private const int NameMaxLength = ValidationConstants.GameProfiles.NameMaxLength;
    
    public string Value { get; }

    public GameName(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new InvalidParameterException("Game name cannot be null nor empty", nameof(value));

        if (value.Length > NameMaxLength)
        {
            throw new InvalidParameterException(
                $"Game name cannot have more than {NameMaxLength} characters",
                nameof(value));
        }

        Value = value;
    }
}

public record DevelopmentInfo
{
    public string Developer { get; }
    public string Publisher { get; }

    public DevelopmentInfo(string developer, string publisher)
    {
        if (string.IsNullOrEmpty(developer))
            throw new InvalidParameterException("Developer name cannot be null nor empty", nameof(developer));
        
        if (string.IsNullOrEmpty(publisher))
            throw new InvalidParameterException("Publisher name cannot be null nor empty", nameof(publisher));
        
        Developer = developer;
        Publisher = publisher;
    }
}