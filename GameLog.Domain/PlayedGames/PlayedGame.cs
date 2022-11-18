using GameLog.Common.PlayedGames;
using GameLog.Domain.Common;
using GameLog.Domain.Common.Exceptions;
using GameLog.Domain.GameProfiles;
using GameLog.Domain.Gamers;

namespace GameLog.Domain.PlayedGames;

public class PlayedGame : Aggregate<Events.PlayedGameEvent>
{
    public PlayedGameId Id { get; private set; }
    public GamerId OwnerGamerId { get; private set; }
    public GameProfileId GameProfileId { get; private set; }
    
    public NonEmptyDateTime CreatedAt { get; private set; }
    
    public PlayedGameStatus? Status { get; private set; }
    public GameScore? Score { get; private set; }
    public NumberOfHoursPlayed HoursPlayed { get; private set; }

    
#pragma warning disable CS8618
    private PlayedGame()
#pragma warning restore CS8618
    {
    }

    public static PlayedGame Create(
        PlayedGameId id,
        GamerId ownerGamerId,
        GameProfileId gameProfileId,
        NonEmptyDateTime createdAt)
    {
        var playedGame = new PlayedGame();
        
        playedGame.Apply(new Events.PlayedGameCreated
        {
            Id = id,
            OwnerGamerId = ownerGamerId,
            GameProfileId = gameProfileId,
            PlayedGameCreatedAt = createdAt
        });

        return playedGame;
    }

    public void UpdateHoursPlayed(NumberOfHoursPlayed hoursPlayed)
    {
        Apply(new Events.HoursPlayedUpdated
        {
            HoursPlayed = hoursPlayed
        });
    }

    public void UpdateScore(GameScore score)
    {
        Apply(new Events.ScoreUpdated
        {
            Score = score
        });
    }

    protected override void When(Events.PlayedGameEvent @event)
    {
        switch (@event)
        {
            case Events.PlayedGameCreated e:
                Id = e.Id;
                OwnerGamerId = e.OwnerGamerId;
                GameProfileId = e.GameProfileId;
                CreatedAt = e.PlayedGameCreatedAt;
                HoursPlayed = NumberOfHoursPlayed.Zero;
                break;
            
            case Events.HoursPlayedUpdated e:
                HoursPlayed = e.HoursPlayed;
                break;
            
            case Events.ScoreUpdated e:
                Score = e.Score;
                break;
            
            default:
                throw new UnrecognizedEventException(@event);
        }
    }

    //TODO: override GetStateValidationErrors to ensure the aggregate is valid
}

public record PlayedGameId : Id
{
    public PlayedGameId(string value) : base(value)
    {
    }
}

public record GameScore
{
    public int Percentage { get; }

    private GameScore(int percentage)
    {
        if (percentage < 0)
            throw new InvalidParameterException("Percentage score cannot be lower than 0", nameof(percentage));
        
        Percentage = percentage;
    }

    public static GameScore From0To100(int value) => new(value);
    public static GameScore From0To10(int value) => new(value * 10);
    public static GameScore From0To5(int value) => new(value * 20);
}

public record NumberOfHoursPlayed
{
    public int Value { get; }

    public NumberOfHoursPlayed(int value)
    {
        if (value < 0)
            throw new InvalidParameterException("Number of hours played cannot be lower than 0", nameof(value));
        
        Value = value;
    }
    
    public static NumberOfHoursPlayed Zero => new(0);
}
