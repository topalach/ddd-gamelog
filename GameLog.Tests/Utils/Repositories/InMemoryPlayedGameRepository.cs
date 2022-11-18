 using System.Linq;
 using System.Threading.Tasks;
 using GameLog.Application.PlayedGames;
 using GameLog.Domain.Gamers;
 using GameLog.Domain.PlayedGames;

 namespace GameLog.Tests.Utils.Repositories;

 public class InMemoryPlayedGameRepository :  InMemoryRepository<PlayedGame>, IPlayedGameRepository
 {
     public Task<PlayedGameId> GetIdAsync()
     {
         var id = new PlayedGameId(GetNextId());
         return Task.FromResult(id);
     }

     public Task StoreAsync(PlayedGame playedGame)
     {
         Items.Add(playedGame);
         return Task.CompletedTask;
     }

     public Task<PlayedGame?> LoadAsync(PlayedGameId id)
     {
         var playedGame = Items.SingleOrDefault(x => x.Id == id);
         return Task.FromResult(playedGame);
     }

     public Task SaveChangesAsync()
     {
         return Task.CompletedTask;
     }

     public Task<NumberOfPlayedGames> GetNumberOfPlayedGamesFor(GamerId gamerId)
     {
         var count = Items.Count(x => x.OwnerGamerId == gamerId);
         return Task.FromResult(new NumberOfPlayedGames(count));
     }
 }