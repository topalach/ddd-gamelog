using GameLog.Common.PlayedGames;

namespace GameLog.Infrastructure.Database.Entities;

public class PlayedGame : DbEntity
{
    public string GamerId { get; set; }
    public Gamer Gamer { get; set; }
    
    public string GameProfileId { get; set; }
    public GameProfile GameProfile { get; set; }
    
    public PlayedGameStatus? Status { get; set; }
    public int PercentageScore { get; set; }
    public int HoursPlayed { get; set; }
}
