using GameLog.Domain.Gamers;

namespace GameLog.Application.Gamers;

public interface IGamerRepository
{
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByNicknameAsync(string nickname);
    Task<GamerId> GetIdAsync();
    Task StoreAsync(Gamer gamer);
    Task<Gamer> LoadAsync(GamerId gamerId);
}