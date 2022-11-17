using GameLog.Application.Utils;
using GameLog.Domain.Gamers;
using GameLog.Domain.PlayedGames;

namespace GameLog.Application.PlayedGames;

public interface IPlayedGameRepository : IRepository<PlayedGameId, PlayedGame>
{
    Task<NumberOfPlayedGames> GetNumberOfPlayedGamesFor(GamerId gamerId);
}