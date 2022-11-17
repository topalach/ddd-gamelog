using GameLog.Common.PlayedGames;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

public class PlayedGameEntityTypeConfiguration : IEntityTypeConfiguration<PlayedGame>
{
    public void Configure(EntityTypeBuilder<PlayedGame> builder)
    {
        builder.ToTable(TableNames.PlayedGames);
    }
}
