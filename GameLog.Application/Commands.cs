namespace GameLog.Application;

public static class Commands
{
    public static class Gamers
    {
        public class UpdateGamerRank
        {
            public string GamerId { get; set; }
        }
    }

    public static class PlayedGames
    {
        public class AddPlayedGameToGamerCollection
        {
            public string GamerId { get; set; }
            public string GameProfileId { get; set; }
        }

        public class SetPlayedGameAsCurrentlyPlaying
        {
            public string PlayedGameId { get; set; }
        }
        
        public class SetPlayedGameAsPlayed
        {
            public string PlayedGameId { get; set; }
            public int? HoursPlayed { get; set; }
        }

        public class SetPlayedGameHoursPlayed
        {
            public string PlayedGameId { get; set; }
            public int HoursPlayed { get; set; }
        }

        public class AddPlayedGamePercentageScore
        {
            public string PlayedGameId { get; set; }
            public int PercentageScore { get; set; }
        }
    }
}