using GameLog.Application.GameProfiles;
using GameLog.Application.Gamers;
using GameLog.Application.Utils;
using GameLog.Domain.Common;
using GameLog.Domain.GameProfiles;
using GameLog.Domain.Gamers;
using GameLog.Domain.PlayedGames;

namespace GameLog.Application.PlayedGames;

public class PlayedGameService
{
    private readonly IPlayedGameRepository _playedGameRepository;
    private readonly IGamerRepository _gamerRepository;
    private readonly IGameProfileRepository _gameProfileRepository;
    
    private readonly ITimeService _timeService;

    public PlayedGameService(
        IPlayedGameRepository playedGameRepository,
        IGamerRepository gamerRepository,
        IGameProfileRepository gameProfileRepository,
        ITimeService timeService)
    {
        _playedGameRepository = playedGameRepository;
        _gamerRepository = gamerRepository;
        _gameProfileRepository = gameProfileRepository;
        _timeService = timeService;
    }

    public async Task<string> Create(Commands.CreatePlayedGame command)
    {
        var gamerId = new GamerId(command.OwnerGamerId);
        var gameProfileId = new GameProfileId(command.GameProfileId);

        await EnsureGamerExists(gamerId);
        await EnsureGameProfileExists(gameProfileId);

        var id = await _playedGameRepository.GetIdAsync();
        var createdAt = _timeService.UtcNow();

        var playedGame = PlayedGame.Create(
            id,
            gamerId,
            gameProfileId,
            new NonEmptyDateTime(createdAt));

        await _playedGameRepository.StoreAsync(playedGame);

        return id.Value;
    }

    public async Task UpdateHoursPlayed(Commands.UpdateHoursPlayed command)
    {
        var playedGame = await LoadPlayedGame(command.PlayedGameId);

        playedGame.UpdateHoursPlayed(new NumberOfHoursPlayed(command.HoursPlayed));

        //TODO: assert changes saved
        await _playedGameRepository.SaveChangesAsync();
    }

    public async Task UpdatePercentageScore(Commands.UpdatePercentageScore command)
    {
        var playedGame = await LoadPlayedGame(command.PlayedGameId);
        
        playedGame.UpdateScore(GameScore.From0To100(command.PercentageScore));

        //TODO: assert changes saved
        await _playedGameRepository.SaveChangesAsync();
    }

    private async Task EnsureGamerExists(GamerId id)
    {
        var exists = await _gamerRepository.ExistsAsync(id);

        if (exists == false)
            throw new InvalidOperationException($"Gamer with ID '{id}' does not exist.");
    }

    private async Task EnsureGameProfileExists(GameProfileId id)
    {
        var exists = await _gameProfileRepository.ExistsAsync(id);

        if (exists == false)
            throw new InvalidOperationException($"Game profile with ID '{id}' does not exist.");
    }

    private async Task<PlayedGame> LoadPlayedGame(string id)
    {
        var playedGame = await _playedGameRepository.LoadAsync(new PlayedGameId(id));

        if (playedGame is null)
            throw new InvalidOperationException($"Played game with ID '{id}' does not exist.");

        return playedGame;
    }
}