using GameLog.Domain.PlayedGames;

namespace GameLog.Application.PlayedGames;

public interface IPlayedGameRepository
{
    Task<PlayedGameId> GetIdAsync();
    Task StoreAsync(PlayedGame playedGame);
    Task<PlayedGame> LoadAsync(PlayedGameId id);
}