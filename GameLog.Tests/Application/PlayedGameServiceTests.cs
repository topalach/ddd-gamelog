using System;
using System.Threading.Tasks;
using GameLog.Application.GameProfiles;
using GameLog.Application.Gamers;
using GameLog.Application.PlayedGames;
using GameLog.Application.Utils;
using GameLog.Domain.Common;
using GameLog.Domain.GameProfiles;
using GameLog.Domain.Gamers;
using GameLog.Domain.PlayedGames;
using GameLog.Tests.Mocks;
using GameLog.Tests.Utils.Repositories;
using Xunit;
using Commands = GameLog.Application.PlayedGames.Commands;

namespace GameLog.Tests.Application;

public class PlayedGameServiceTests
{
    private readonly IGamerRepository _gamerRepository = new InMemoryGamerRepository();
    private readonly IPlayedGameRepository _playedGameRepository = new InMemoryPlayedGameRepository();
    private readonly IGameProfileRepository _gameProfileRepository = new InMemoryGameProfileRepository();
    
    private readonly ITimeService _timeService = new MockTimeService();
    
    [Fact]
    public async Task CreateSucceeds_WhenGamer_AndGameProfileExist()
    {
        var gamerId = await CreateSomeGamer();
        var gameProfileId = await CreateSomeGameProfile();

        var command = new Commands.CreatePlayedGame
        {
            OwnerGamerId = gamerId.Value,
            GameProfileId = gameProfileId.Value
        };
        
        var sut = GetSut();

        var id = await sut.Create(command);

        var created = await AssertPlayedGameInRepository(id);
        Assert.Equal(MockTimeService.DefaultNonEmptyUtcNow, created.CreatedAt);
    }

    [Fact]
    public async Task CreateFails_WhenGamerDoesNotExist()
    {
        var gameProfileId = await CreateSomeGameProfile();

        var command = new Commands.CreatePlayedGame
        {
            OwnerGamerId = "gamer-id-does-not-exist",
            GameProfileId = gameProfileId.Value
        };
        
        var sut = GetSut();

        await Assert.ThrowsAsync<InvalidOperationException>(() => sut.Create(command));
    }

    [Fact]
    public async Task CreateFails_WhenGameProfileDoesNotExist()
    {
        var gamerId = await CreateSomeGamer();

        var command = new Commands.CreatePlayedGame
        {
            OwnerGamerId = gamerId.Value,
            GameProfileId = "game-profile-id-does-not-exist"
        };
        
        var sut = GetSut();

        await Assert.ThrowsAsync<InvalidOperationException>(() => sut.Create(command));
    }

    [Fact]
    public async Task CreateFails_WhenAddingPlayedGame_ForTheSameGamerId_AndGameProfileId()
    {
        var gamerId = await CreateSomeGamer();
        var gameProfileId = await CreateSomeGameProfile();

        var command = new Commands.CreatePlayedGame
        {
            OwnerGamerId = gamerId.Value,
            GameProfileId = gameProfileId.Value
        };
        
        var sut = GetSut();

        await sut.Create(command);
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => sut.Create(command));
    }

    [Fact]
    public async Task CreateSucceeds_WhenPlayedGameAlreadyExists_WithTheSameGamerId_ButAnotherGameProfileId()
    {
        var gamerId = await CreateSomeGamer();
        
        var gameProfileId1 = await CreateSomeGameProfile();
        var gameProfileId2 = await CreateSomeGameProfile();

        var command1 = new Commands.CreatePlayedGame
        {
            OwnerGamerId = gamerId.Value,
            GameProfileId = gameProfileId1.Value
        };
        
        var command2 = new Commands.CreatePlayedGame
        {
            OwnerGamerId = gamerId.Value,
            GameProfileId = gameProfileId2.Value
        };
        
        var sut = GetSut();

        var id1 = await sut.Create(command1);
        await AssertPlayedGameInRepository(id1);

        var id2 = await sut.Create(command2);
        await AssertPlayedGameInRepository(id2);
    }

    [Fact]
    public async Task CreateSucceeds_WhenPlayedGameAlreadyExists_WithTheSameGameProfileId_ButAnotherGamerId()
    {
        var gamerId1 = await CreateSomeGamer();
        var gamerId2 = await CreateSomeGamer();
        
        var gameProfileId = await CreateSomeGameProfile();

        var command1 = new Commands.CreatePlayedGame
        {
            OwnerGamerId = gamerId1.Value,
            GameProfileId = gameProfileId.Value
        };
        
        var command2 = new Commands.CreatePlayedGame
        {
            OwnerGamerId = gamerId2.Value,
            GameProfileId = gameProfileId.Value
        };
        
        var sut = GetSut();

        var id1 = await sut.Create(command1);
        await AssertPlayedGameInRepository(id1);

        var id2 = await sut.Create(command2);
        await AssertPlayedGameInRepository(id2);
    }

    [Fact]
    public async Task UpdateHoursPlayed_Succeeds_WhenPlayedGameExists()
    {
        const int hoursPlayed = 3;
        var playedGameId = await CreateSomePlayedGame();

        var command = new Commands.UpdateHoursPlayed
        {
            PlayedGameId = playedGameId.Value,
            HoursPlayed = hoursPlayed
        };

        var sut = GetSut();

        await sut.UpdateHoursPlayed(command);

        var playedGame = await _playedGameRepository.LoadAsync(playedGameId);
        
        Assert.NotNull(playedGame);
        Assert.Equal(hoursPlayed, playedGame.HoursPlayed.Value);
    }

    [Fact]
    public async Task UpdateHoursPlayed_Fails_WhenPlayedGameDoesNotExist()
    {
        const int someHoursPlayed = 3;

        var command = new Commands.UpdateHoursPlayed
        {
            PlayedGameId = "played-game-does-not-exist",
            HoursPlayed = someHoursPlayed
        };

        var sut = GetSut();

        await Assert.ThrowsAsync<InvalidOperationException>(() => sut.UpdateHoursPlayed(command));
    }

    [Fact]
    public async Task UpdatePercentageScore_Succeeds_WhenPlayedGameExists()
    {
        const int percentageScore = 75;
        var playedGameId = await CreateSomePlayedGame();

        var command = new Commands.UpdatePercentageScore
        {
            PlayedGameId = playedGameId.Value,
            PercentageScore = percentageScore
        };

        var sut = GetSut();

        await sut.UpdatePercentageScore(command);

        var playedGame = await _playedGameRepository.LoadAsync(playedGameId);
        
        Assert.NotNull(playedGame);
        Assert.NotNull(playedGame.Score);
        
        Assert.Equal(percentageScore, playedGame.Score.Percentage);
    }

    [Fact]
    public async Task UpdatePercentageScore_Fails_WhenPlayedGameDoesNotExist()
    {
        const string playedGameId = "played-game-does-not-exist";
        const int someHoursPlayed = 3;

        var command = new Commands.UpdatePercentageScore
        {
            PlayedGameId = "played-game-does-not-exist",
            PercentageScore = someHoursPlayed
        };

        var sut = GetSut();

        await Assert.ThrowsAsync<InvalidOperationException>(() => sut.UpdatePercentageScore(command));
    }

    private async Task<GamerId> CreateSomeGamer()
    {
        var id = await _gamerRepository.GetIdAsync();

        var gamer = Gamer.Create(
            id,
            new Email("some.gamer.email@example.com"),
            new Nickname("some-nickname"),
            SomeCreatedAtDate);

        await _gamerRepository.StoreAsync(gamer);

        return id;
    }

    private async Task<GameProfileId> CreateSomeGameProfile()
    {
        var id = await _gameProfileRepository.GetIdAsync();

        var gameProfile = GameProfile.Create(
            id,
            new GameName("Some Name"),
            new Genre("RTS"),
            new DevelopmentInfo("Some Developer", "Some Publisher"),
            new GameProfileDescription("Some description"),
            SomeCreatedAtDate);

        await _gameProfileRepository.StoreAsync(gameProfile);

        return id;
    }

    private async Task<PlayedGameId> CreateSomePlayedGame()
    {
        var id = await _playedGameRepository.GetIdAsync();

        var playedGame = PlayedGame.Create(
            id,
            new GamerId("some-gamer-id"),
            new GameProfileId("some-game-profile-id"),
            SomeCreatedAtDate);

        await _playedGameRepository.StoreAsync(playedGame);

        return id;
    }

    private static NonEmptyDateTime SomeCreatedAtDate => new(new DateTimeOffset(2022, 11, 15, 0, 0, 0, TimeSpan.Zero));

    private PlayedGameService GetSut()
        => new(
            _playedGameRepository,
            _gamerRepository,
            _gameProfileRepository,
            _timeService);

    private async Task<PlayedGame> AssertPlayedGameInRepository(string id)
    {
        var playedGame = await _playedGameRepository.LoadAsync(new PlayedGameId(id));
        
        Assert.NotNull(playedGame);
        return playedGame;
    }
}