using GameLog.Common.Gamers;

namespace GameLog.Infrastructure.Database.Entities;

public class Gamer : DbEntity
{
    public string Email { get; set; }
    public string Nickname { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public GamerRank Rank { get; set; }
    public int NumberOfPlayedGames { get; set; }
    
    public List<PlayedGame> PlayedGames { get; set; }
}
