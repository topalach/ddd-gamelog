using GameLog.Application.Utils;
using GameLog.Domain.GameProfiles;

namespace GameLog.Application.GameProfiles;

public interface IGameProfileRepository : IRepository<GameProfileId, GameProfile>
{
    Task<bool> ExistsAsync(GameProfileId id);
    Task<bool> NameExistsAsync(string gameProfileName);
}