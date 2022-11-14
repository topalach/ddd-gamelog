namespace GameLog.Application.PlayedGames;

public static class Commands
{
    public class CreatePlayedGame
    {
        public string OwnerGamerId { get; set; }
        public string GameProfileId { get; set; }
    }
    
    public class UpdateHoursPlayed
    {
        public string PlayedGameId { get; set; }
        public int HoursPlayed { get; set; }
    }

    public class UpdatePercentageScore
    {
        public string PlayedGameId { get; set; }
        public int PercentageScore { get; set; }
    }
}