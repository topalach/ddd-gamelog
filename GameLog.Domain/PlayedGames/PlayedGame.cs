using GameLog.Domain.Actors;
using GameLog.Domain.Common;
using GameLog.Domain.Common.Exceptions;
using GameLog.Domain.GameProfiles;

namespace GameLog.Domain.PlayedGames;

public class PlayedGame
{
    public PlayedGameId Id { get; }
    public GamerId OwnerGamerId { get; }
    public GameProfileId GameProfileId { get; }
    
    public NonEmptyDateTime CreatedAt { get; }
    public NonEmptyDateTime? UpdatedAt { get; }
    
    public PlayedGameStatus? Status { get; }
    public GameScore? Score { get; }
    public NumberOfHoursPlayed? HoursPlayed { get; }

    public PlayedGame(PlayedGameId id, GamerId ownerGamerId, GameProfileId gameProfileId, NonEmptyDateTime createdAt)
    {
        Id = id;
        OwnerGamerId = ownerGamerId;
        GameProfileId = gameProfileId;
        CreatedAt = createdAt;
    }
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
}

public enum PlayedGameStatus
{
    CurrentlyPlaying,
    Played
}