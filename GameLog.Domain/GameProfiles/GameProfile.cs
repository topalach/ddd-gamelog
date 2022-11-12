using GameLog.Common.Validation;
using GameLog.Domain.Common;
using GameLog.Domain.Common.Exceptions;

namespace GameLog.Domain.GameProfiles;

public class GameProfile : Aggregate<Events.GameProfileEvent>
{
    public GameProfileId Id { get; private set; }
    public GameName Name { get; private set; }
    public Genre Genre { get; private set; }
    public DevelopmentInfo DevelopmentInfo { get; private set; }
    public GameProfileDescription Description { get; private set; }
    public NonEmptyDateTime CreatedAt { get; private set; }

#pragma warning disable CS8618
    private GameProfile()
#pragma warning restore CS8618
    {
    }

    public static GameProfile Create(
        GameProfileId id,
        GameName name,
        Genre genre,
        DevelopmentInfo developmentInfo,
        GameProfileDescription description,
        NonEmptyDateTime createdAt)
    {
        var gameProfile = new GameProfile();

        gameProfile.Apply(
            new Events.GameProfileCreated
            {
                Id = id,
                GameProfileName = name,
                Genre = genre,
                DevelopmentInfo = developmentInfo,
                Description = description,
                GameProfileCreatedAt = createdAt
            });

        return gameProfile;
    }

    protected override void When(Events.GameProfileEvent @event)
    {
        switch (@event)
        {
            case Events.GameProfileCreated e:
                Id = e.Id;
                Name = e.GameProfileName;
                Genre = e.Genre;
                DevelopmentInfo = e.DevelopmentInfo;
                Description = e.Description;
                CreatedAt = e.GameProfileCreatedAt;
                break;
            
            default:
                throw new UnrecognizedEventException(@event);
        }
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

public record Genre
{
    public string Value { get; }

    public Genre(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new InvalidParameterException("Genre cannot be null nor empty", nameof(value));
        
        Value = value;
    }
}

public record GameProfileDescription
{
    private const int MaxLength = ValidationConstants.GameProfiles.DescriptionMaxLength;
    
    public string Value { get; }

    public GameProfileDescription(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new InvalidParameterException("Game profile description cannot be null nor empty", nameof(value));

        if (value.Length > MaxLength)
        {
            throw new InvalidParameterException(
                $"Game profile description cannot contain more than {MaxLength} characters",
                nameof(value));
        }

        Value = value;
    }

    public static GameProfileDescription ToBeAnnounced() => new GameProfileDescription("TBA");
}