using GameLog.Application.PlayedGames;
using GameLog.Application.Utils;
using GameLog.Domain.Common;
using GameLog.Domain.Gamers;

namespace GameLog.Application.Gamers;

public class GamerService
{
    private readonly IGamerRepository _gamerRepository;
    private readonly IPlayedGameRepository _playedGameRepository;
    
    private readonly ITimeService _timeService;

    public GamerService(
        IGamerRepository gamerRepository,
        IPlayedGameRepository playedGameRepository,
        ITimeService timeService)
    {
        _gamerRepository = gamerRepository;
        _playedGameRepository = playedGameRepository;
        _timeService = timeService;
    }

    public async Task<string> CreateGamer(Commands.AddGamer command)
    {
        var emailTaken = await _gamerRepository.ExistsByEmailAsync(new Email(command.Email));

        if (emailTaken)
            throw new InvalidOperationException($"Gamer with email '{command.Email}' was already added.");

        var nicknameTaken = await _gamerRepository.ExistsByNicknameAsync(new Nickname(command.Nickname));
        
        if (nicknameTaken)
            throw new InvalidOperationException($"Gamer with nickname '{command.Nickname}' was already added.");
        
        //TODO: check librarian nicknames and emails as well

        var id = await _gamerRepository.GetIdAsync();
        var createdAt = new NonEmptyDateTime(_timeService.UtcNow());
        
        var gamer = Gamer.Create(
            id,
            new Email(command.Email),
            new Nickname(command.Nickname),
            createdAt);

        await _gamerRepository.StoreAsync(gamer);
        return id.Value;
    }

    public async Task UpdateFullName(Commands.UpdateFullName command)
    {
        var gamer = await LoadGamer(command.GamerId);

        var fullName = new FullName(command.FirstName, command.LastName);
        gamer.UpdateFullName(fullName);

        await _gamerRepository.SaveChangesAsync();
    }

    private async Task<Gamer> LoadGamer(string id)
    {
        var gamer = await _gamerRepository.LoadAsync(new GamerId(id));

        if (gamer is null)
            throw new InvalidOperationException($"Could not find a gamer with ID {id}.");

        return gamer;
    }

    public async Task UpdateNumberOfPlayedGames(Commands.UpdateNumberOfPlayedGames command)
    {
        var gamer = await LoadGamer(command.GamerId);

        var numberOfPlayedGames = await _playedGameRepository.GetNumberOfPlayedGamesFor(new GamerId(command.GamerId));
        gamer.UpdateNumberOfPlayedGames(numberOfPlayedGames);

        await _gamerRepository.SaveChangesAsync();
    }
}