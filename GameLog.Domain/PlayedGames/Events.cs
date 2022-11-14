using GameLog.Domain.Common;
using GameLog.Domain.GameProfiles;
using GameLog.Domain.Gamers;

#pragma warning disable CS8618

namespace GameLog.Domain.PlayedGames;

public static class Events
{
    public abstract class PlayedGameEvent : Event
    {
    }
    
    public class PlayedGameCreated : PlayedGameEvent
    {
        public override string Name() => nameof(PlayedGameCreated);
        
        public PlayedGameId Id { get; set; }
        public GamerId OwnerGamerId { get; set; }
        public GameProfileId GameProfileId { get; set; }

        public NonEmptyDateTime CreatedAt { get; set; }
    }

    public class ScoreUpdated : PlayedGameEvent
    {
        public override string Name() => nameof(ScoreUpdated);

        public GameScore Score { get; set; }
    }

    public class HoursPlayedUpdated : PlayedGameEvent
    {
        public override string Name() => nameof(HoursPlayedUpdated);
        
        public NumberOfHoursPlayed HoursPlayed { get; set; }
    }
}