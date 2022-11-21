using GameLog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace GameLog.Web.Configuration;

public static class DbContextExtensions
{
    public static void AddGameLogDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<GameLogDbContext>(options => options.UseSqlServer(connectionString));
    }
}