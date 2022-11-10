using GameLog.Common.Gamers;

namespace GameLog.Messages;

public static class Events
{
    public static class Gamers
    {
        public class NewRankAchieved
        {
            public string GamerId { get; set; }
            public GamerRank NewRank { get; set; }
        }
    }
    
    public static class PlayedGames
    {
        public class PlayedGameAddedToGamerCollection
        {
            public string GamerId { get; set; }
            public string PlayedGameId { get; set; }
        }
        
        public class ScoreUpdated
        {
            public string GameProfileId { get; set; }
            public int PercentageScore { get; set; }
        }

        public class HoursPlayedUpdated
        {
            public string GameProfileId { get; set; }
            public int HoursPlayed { get; set; }
        }
    }
}