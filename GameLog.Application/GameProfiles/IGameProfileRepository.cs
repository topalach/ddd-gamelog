using GameLog.Domain.GameProfiles;

namespace GameLog.Application.GameProfiles;

public interface IGameProfileRepository
{
    Task<GameProfileId> GetIdAsync();
    Task StoreAsync(GameProfile gameProfile);
    Task<GameProfile> LoadAsync(GameProfileId id);
    Task SaveAsync(GameProfile gameProfile);
    Task<bool> NameExistsAsync(string gameProfileName);
}