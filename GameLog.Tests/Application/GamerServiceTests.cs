using System;
using System.Threading.Tasks;
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

using Commands = GameLog.Application.Gamers.Commands;

namespace GameLog.Tests.Application;

public class GamerServiceTests
{
    private readonly IGamerRepository _gamerRepository = new InMemoryGamerRepository();
    private readonly IPlayedGameRepository _playedGameRepository = new InMemoryPlayedGameRepository();
    
    private readonly ITimeService _timeService = new MockTimeService();

    [Fact]
    public async Task CreatesGamer()
    {
        const string email = "john@example.com";
        const string nickname = "johnDoe";
        
        var sut = GetSut();

        var id = await sut.CreateGamer(
            new Commands.AddGamer
            {
                Email = email,
                Nickname = nickname
            });

        var gamer = await _gamerRepository.LoadAsync(new GamerId(id));
        
        Assert.NotNull(gamer);
        
        Assert.Equal(new Email(email), gamer.Email);
        Assert.Equal(new Nickname(nickname), gamer.Nickname);
        Assert.Equal(MockTimeService.DefaultUtcNow, gamer.CreatedAt.Value);
    }

    [Fact]
    public async Task CreateFails_WhenEmailAlreadyTaken()
    {
        const string email = "john@example.com";
        const string nickname1 = "nickname1";
        const string nickname2 = "nickname2";
        
        var sut = GetSut();

        var command1 = new Commands.AddGamer
        {
            Email = email,
            Nickname = nickname1
        };

        var command2 = new Commands.AddGamer
        {
            Email = email,
            Nickname = nickname2
        };
        
        await sut.CreateGamer(command1);

        await Assert.ThrowsAsync<InvalidOperationException>(() => sut.CreateGamer(command2));
    }

    [Fact]
    public async Task CreateFails_WhenNicknameAlreadyTaken()
    {
        const string email1 = "john@example.com";
        const string email2 = "alice@example.com";
        const string nickname = "nickname";
        
        var sut = GetSut();

        var command1 = new Commands.AddGamer
        {
            Email = email1,
            Nickname = nickname
        };

        var command2 = new Commands.AddGamer
        {
            Email = email2,
            Nickname = nickname
        };
        
        await sut.CreateGamer(command1);

        await Assert.ThrowsAsync<InvalidOperationException>(() => sut.CreateGamer(command2));
    }

    [Fact]
    public async Task UpdateFullName_Succeeds()
    {
        var gamer = await StoreSomeGamer();

        Assert.Null(gamer.FullName);
        
        var sut = GetSut();

        await sut.UpdateFullName(
            new Commands.UpdateFullName
            {
                GamerId = gamer.Id.Value,
                FirstName = "first",
                LastName = "last"
            });
        
        Assert.Equal(new FullName("first", "last"), gamer.FullName);
    }

    [Fact]
    public async Task UpdateFullName_Fails_WhenGamerDoesNotExist()
    {
        var sut = GetSut();

        var command = new Commands.UpdateFullName
        {
            GamerId = "does-not-exist",
            FirstName = "first",
            LastName = "last"
        };
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => sut.UpdateFullName(command));
    }

    [Fact]
    public async Task UpdateNumberOfPlayedGames_Succeeds_WhenMultiplePlayedGames()
    {
        var gamer = await StoreSomeGamer();
        Assert.Equal(NumberOfPlayedGames.Zero, gamer.NumberOfPlayedGames);

        await StoreSomePlayedGame(gamer.Id, 1);
        await StoreSomePlayedGame(gamer.Id, 2);
        await StoreSomePlayedGame(gamer.Id, 3);
        
        var sut = GetSut();

        await sut.UpdateNumberOfPlayedGames(new Commands.UpdateNumberOfPlayedGames
        {
            GamerId = gamer.Id.Value
        });
        
        Assert.Equal(new NumberOfPlayedGames(3), gamer.NumberOfPlayedGames);
    }

    private Task StoreSomePlayedGame(GamerId gamerId, int ordinal)
    {
        var playedGameId = new PlayedGameId($"played-game-id-{ordinal}");
        var gameProfileId = new GameProfileId($"game-profile-id-{ordinal}");
        
        var playedGame = PlayedGame.Create(
            playedGameId,
            gamerId,
            gameProfileId,
            SomeCreatedAtDate);

        return _playedGameRepository.StoreAsync(playedGame);
    }

    [Fact]
    public async Task UpdateNumberOfPlayedGames_Succeeds_WhenNoPlayedGames()
    {
        var gamer = await StoreSomeGamer();
        Assert.Equal(NumberOfPlayedGames.Zero, gamer.NumberOfPlayedGames);
        
        var sut = GetSut();

        await sut.UpdateNumberOfPlayedGames(new Commands.UpdateNumberOfPlayedGames
        {
            GamerId = gamer.Id.Value
        });
        
        Assert.Equal(NumberOfPlayedGames.Zero, gamer.NumberOfPlayedGames);
    }

    [Fact]
    public async Task UpdateNumberOfPlayedGames_Fails_WhenGamerDoesNotExist()
    {
        var gamer = await StoreSomeGamer();
        Assert.Equal(NumberOfPlayedGames.Zero, gamer.NumberOfPlayedGames);
        
        var sut = GetSut();

        var command = new Commands.UpdateNumberOfPlayedGames
        {
            GamerId = "does-not-exist"
        };
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => sut.UpdateNumberOfPlayedGames(command));
    }

    private static readonly GamerId DefaultGamerId = new("88443a04-47f1-49b3-b94e-b7a6df2d3dc2");
    private static NonEmptyDateTime SomeCreatedAtDate => new(new DateTimeOffset(2022, 11, 14, 0, 0, 0, TimeSpan.Zero));

    private async Task<Gamer> StoreSomeGamer()
    {
        var gamer = GetSomeGamer();
        await _gamerRepository.StoreAsync(gamer);
        return gamer;
    }

    private static Gamer GetSomeGamer()
        => Gamer.Create(
            DefaultGamerId,
            new Email("some@example.com"),
            new Nickname("someNickname"),
            MockTimeService.DefaultNonEmptyUtcNow);

    private GamerService GetSut() => new(_gamerRepository, _playedGameRepository, _timeService);
}