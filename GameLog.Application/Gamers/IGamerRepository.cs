using GameLog.Application.Utils;
using GameLog.Domain.Common;
using GameLog.Domain.Gamers;

namespace GameLog.Application.Gamers;

public interface IGamerRepository : IRepository<GamerId, Gamer>
{
    Task<bool> ExistsAsync(GamerId id);
    Task<bool> ExistsByEmailAsync(Email email);
    Task<bool> ExistsByNicknameAsync(Nickname nickname);
}