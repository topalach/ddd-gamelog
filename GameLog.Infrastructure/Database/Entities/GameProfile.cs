namespace GameLog.Infrastructure.Database.Entities;

public class GameProfile : DbEntity
{
    public string Name { get; set; }
    public string Genre { get; set; }
    public string Developer { get; set; }
    public string Publisher { get; set; }
    public string Description { get; set; }
}
