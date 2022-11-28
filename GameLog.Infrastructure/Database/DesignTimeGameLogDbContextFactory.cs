using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GameLog.Infrastructure.Database;

// ReSharper disable once UnusedType.Global
public class DesignTimeGameLogDbContextFactory : IDesignTimeDbContextFactory<GameLogDbContext>
{
    public GameLogDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<GameLogDbContext>();
        optionsBuilder.UseSqlServer();
        
        return new GameLogDbContext(optionsBuilder.Options);
    }
}