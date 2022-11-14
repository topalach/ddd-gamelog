using GameLog.Domain.GameProfiles;

namespace GameLog.Application.GameProfiles;

public interface IGameProfileRepository
{
    Task<bool> ExistsAsync(GameProfileId id);
    Task<bool> NameExistsAsync(string gameProfileName);
    
    Task<GameProfileId> GetIdAsync();
    Task StoreAsync(GameProfile gameProfile);
    Task<GameProfile?> LoadAsync(GameProfileId id);
    Task SaveChangesAsync();
}