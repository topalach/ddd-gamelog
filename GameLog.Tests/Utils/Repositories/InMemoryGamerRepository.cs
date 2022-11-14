 using System.Linq;
 using System.Threading.Tasks;
 using GameLog.Application.Gamers;
 using GameLog.Domain.Common;
 using GameLog.Domain.Gamers;

 namespace GameLog.Tests.Utils.Repositories;

 public class InMemoryGamerRepository : InMemoryRepository<Gamer>, IGamerRepository
 {
     public Task<bool> ExistsByEmailAsync(string email)
     {
         var expectedEmail = new Email(email);
         var exists = Items.Any(x => x.Email == expectedEmail);
         
         return Task.FromResult(exists);
     }

     public Task<bool> ExistsByNicknameAsync(string nickname)
     {
         var expectedNickname = new Nickname(nickname);
         var exists = Items.Any(x => x.Nickname == expectedNickname);
         
         return Task.FromResult(exists);
     }

     public Task<GamerId> GetIdAsync()
     {
         var gamerId = new GamerId(GetNextId());
         return Task.FromResult(gamerId);
     }

     public Task StoreAsync(Gamer gamer)
     {
         Items.Add(gamer);
         return Task.CompletedTask;
     }

     public Task<Gamer> LoadAsync(GamerId gamerId)
     {
         var gamer = Items.Single(x => x.Id == gamerId);
         return Task.FromResult(gamer);
     }
 }
