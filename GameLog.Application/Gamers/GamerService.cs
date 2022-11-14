using GameLog.Application.Utils;
using GameLog.Domain.Common;
using GameLog.Domain.Gamers;

namespace GameLog.Application.Gamers;

public class GamerService
{
    private readonly IGamerRepository _gamerRepository;
    private readonly ITimeService _timeService;

    public GamerService(IGamerRepository gamerRepository, ITimeService timeService)
    {
        _gamerRepository = gamerRepository;
        _timeService = timeService;
    }

    public async Task<string> CreateGamer(Commands.AddGamer command)
    {
        var emailTaken = await _gamerRepository.ExistsByEmailAsync(command.Email);

        if (emailTaken)
            throw new InvalidOperationException($"Gamer with email '{command.Email}' was already added.");

        var nicknameTaken = await _gamerRepository.ExistsByNicknameAsync(command.Nickname);
        
        if (nicknameTaken)
            throw new InvalidOperationException($"Gamer with nickname '{command.Nickname}' was already added.");

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
        throw new System.NotImplementedException("TODO");
    }

    public async Task UpdateNumberOfPlayedGames(Commands.UpdateNumberOfPlayedGames command)
    {
        throw new System.NotImplementedException("TODO");
    }
}