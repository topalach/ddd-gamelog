﻿ using System.Linq;
 using System.Threading.Tasks;
 using GameLog.Application.Gamers;
 using GameLog.Domain.Common;
 using GameLog.Domain.Gamers;

 namespace GameLog.Tests.Utils.Repositories;

 public class InMemoryGamerRepository : InMemoryRepository<Gamer>, IGamerRepository
 {
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

     public Task<Gamer?> LoadAsync(GamerId gamerId)
     {
         var gamer = Items.SingleOrDefault(x => x.Id == gamerId);
         return Task.FromResult(gamer);
     }

     public Task SaveChangesAsync()
     {
         return Task.CompletedTask;
     }
     
     public Task<bool> ExistsAsync(GamerId id)
     {
         var exists = Items.Any(x => x.Id == id);
         return Task.FromResult(exists);
     }

     public Task<bool> ExistsByEmailAsync(Email email)
     {
         var exists = Items.Any(x => x.Email == email);
         return Task.FromResult(exists);
     }

     public Task<bool> ExistsByNicknameAsync(Nickname nickname)
     {
         var exists = Items.Any(x => x.Nickname == nickname);
         return Task.FromResult(exists);
     }
 }
