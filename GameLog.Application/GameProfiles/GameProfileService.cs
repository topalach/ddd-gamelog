using GameLog.Application.Utils;
using GameLog.Domain.Common;
using GameLog.Domain.GameProfiles;

namespace GameLog.Application.GameProfiles;

public class GameProfileService
{
    private readonly IGameProfileRepository _repository;
    private readonly ITimeService _timeService;

    public GameProfileService(IGameProfileRepository repository, ITimeService timeService)
    {
        _repository = repository;
        _timeService = timeService;
    }

    public async Task<string> AddGameProfile(Commands.AddGameProfile command)
    {
        var wasAlreadyAdded = await _repository.NameExistsAsync(command.Name);

        if (wasAlreadyAdded)
            throw new InvalidOperationException($"Game profile with name '{command.Name}' already exists.");

        var id = await _repository.GetIdAsync();
        var createdAt = new NonEmptyDateTime(_timeService.UtcNow());
        
        var gameProfile = GameProfile.Create(
            id,
            new GameName(command.Name),
            new Genre(command.Genre),
            new DevelopmentInfo(command.Developer, command.Publisher),
            new GameProfileDescription(command.Description),
            createdAt);

        await _repository.StoreAsync(gameProfile);

        return id.Value;
    }
}