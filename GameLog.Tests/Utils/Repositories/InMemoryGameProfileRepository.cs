using System.Linq;
using System.Threading.Tasks;
using GameLog.Application.GameProfiles;
using GameLog.Domain.GameProfiles;

namespace GameLog.Tests.Utils.Repositories;

public class InMemoryGameProfileRepository: InMemoryRepository<GameProfile>, IGameProfileRepository
{
    public Task<GameProfileId> GetIdAsync()
    {
        var id = new GameProfileId(GetNextId());
        return Task.FromResult(id);
    }

    public Task StoreAsync(GameProfile gameProfile)
    {
        Items.Add(gameProfile);
        return Task.CompletedTask;
    }

    public Task<GameProfile?> LoadAsync(GameProfileId id)
    {
        var item = Items.SingleOrDefault(x => x.Id == id);
        return Task.FromResult(item);
    }

    public Task SaveChangesAsync()
    {
        return Task.CompletedTask;
    }
    
    public Task<bool> ExistsAsync(GameProfileId id)
    {
        var exists = Items.Any(x => x.Id == id);
        return Task.FromResult(exists);
    }

    public Task<bool> NameExistsAsync(string gameProfileName)
    {
        var expectedGameName = new GameName(gameProfileName);
        var exists = Items.Any(x => x.Name == expectedGameName);
        
        return Task.FromResult(exists);
    }
}