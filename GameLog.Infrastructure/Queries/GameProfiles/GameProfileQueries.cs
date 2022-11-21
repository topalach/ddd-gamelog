using GameLog.Infrastructure.Database;
using GameLog.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameLog.Infrastructure.Queries.GameProfiles;

public static class GameProfileQueries
{
    public static Task<List<ReadModels.GuestListItem>> GetGuestList(
        GameLogDbContext dbContext,
        GuestListQueryParams parameters)
    {
        var query = from gameProfile in dbContext.Set<GameProfile>()
                    join playedGame in dbContext.Set<PlayedGame>()
                        on gameProfile.Id equals playedGame.GameProfileId into gameProfileWithPlayedGames
                    from playedGame in gameProfileWithPlayedGames.DefaultIfEmpty()
                    select new ReadModels.GuestListItem
                    {
                        Id = gameProfile.Id,
                        Name = gameProfile.Name,
                        Genre = gameProfile.Genre,
                        AveragePercentageScore = gameProfileWithPlayedGames.Average(x => x.PercentageScore),
                        AverageHoursPlayed = gameProfileWithPlayedGames.Average(x => x.HoursPlayed)
                    };
        
        return query
            .Skip(parameters.Skip)
            .Take(parameters.Take)
            .ToListAsync();
    }

    public class GuestListQueryParams : PagedQueryParams
    {
    }
}