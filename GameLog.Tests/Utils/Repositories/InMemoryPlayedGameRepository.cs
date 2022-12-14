 using System.Linq;
 using System.Threading.Tasks;
 using GameLog.Application.PlayedGames;
 using GameLog.Domain.GameProfiles;
 using GameLog.Domain.Gamers;
 using GameLog.Domain.PlayedGames;

 namespace GameLog.Tests.Utils.Repositories;

 public class InMemoryPlayedGameRepository :  InMemoryRepository<PlayedGameId, PlayedGame>, IPlayedGameRepository
 {
     public Task<PlayedGameId> GetIdAsync()
     {
         var id = new PlayedGameId(GetNextId());
         return Task.FromResult(id);
     }

     public Task<NumberOfPlayedGames> GetNumberOfPlayedGamesFor(GamerId gamerId)
     {
         var count = Items.Count(x => x.OwnerGamerId == gamerId);
         return Task.FromResult(new NumberOfPlayedGames(count));
     }

     public Task<bool> ExistsForGamerAndGameProfile(GamerId gamerId, GameProfileId gameProfileId)
     {
         var exists = Items.Any(x => x.OwnerGamerId == gamerId && x.GameProfileId == gameProfileId);
         return Task.FromResult(exists);
     }
 }