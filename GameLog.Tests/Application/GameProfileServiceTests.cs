using System;
using System.Threading.Tasks;
using GameLog.Application.GameProfiles;
using GameLog.Domain.Common.Exceptions;
using GameLog.Domain.GameProfiles;
using GameLog.Tests.Mocks;
using GameLog.Tests.Utils.Repositories;
using Xunit;
#pragma warning disable CS8625

namespace GameLog.Tests.Application;

public class GameProfileServiceTests
{
    private readonly IGameProfileRepository _gameProfileRepository = new InMemoryGameProfileRepository();
    private readonly MockTimeService _mockTimeService = new();
    
    [Fact]
    public async Task AddsGameProfile_WhenNoneExistsYet()
    {
        var sut = GetSut();

        var id = await sut.AddGameProfile(CorrectAddGameProfileCommand);

        var gameProfile = await _gameProfileRepository.LoadAsync(new GameProfileId(id));
        
        Assert.NotNull(gameProfile);
        Assert.Equal(new GameName(SomeGameProfileName), gameProfile.Name);
        
        Assert.NotNull(gameProfile.CreatedAt);
        Assert.Equal(MockTimeService.DefaultUtcNow, gameProfile.CreatedAt.Value);
    }

    [Fact]
    public async Task Fails_WhenGameProfile_WithTheSameName_AlreadyAdded()
    {
        const string name = "Game profile name";

        var command1 = new Commands.AddGameProfile
        {
            Name = name,
            Description = "description 1",
            Developer = "developer 1",
            Publisher = "publisher 1",
            Genre = "genre 1"
        };
        
        var command2 = new Commands.AddGameProfile
        {
            Name = name,
            Description = "description 2",
            Developer = "developer 2",
            Publisher = "publisher 2",
            Genre = "genre 2"
        };

        var sut = GetSut();

        await sut.AddGameProfile(command1);

        await Assert.ThrowsAsync<InvalidOperationException>(() => sut.AddGameProfile(command2));
    }

    [Fact]
    public async Task Fails_WhenNameIsNull()
    {
        var command = CorrectAddGameProfileCommand;
        command.Name = null;
        
        var sut = GetSut();

        await Assert.ThrowsAsync<InvalidParameterException>(() => sut.AddGameProfile(command));
    }

    [Fact]
    public async Task Fails_WhenDescriptionIsNull()
    {
        var command = CorrectAddGameProfileCommand;
        command.Description = null;
        
        var sut = GetSut();

        await Assert.ThrowsAsync<InvalidParameterException>(() => sut.AddGameProfile(command));
    }

    [Fact]
    public async Task Fails_WhenGenreIsNull()
    {
        var command = CorrectAddGameProfileCommand;
        command.Genre = null;
        
        var sut = GetSut();

        await Assert.ThrowsAsync<InvalidParameterException>(() => sut.AddGameProfile(command));
    }

    [Fact]
    public async Task Fails_WhenDeveloperIsNull()
    {
        var command = CorrectAddGameProfileCommand;
        command.Developer = null;
        
        var sut = GetSut();

        await Assert.ThrowsAsync<InvalidParameterException>(() => sut.AddGameProfile(command));
    }

    [Fact]
    public async Task Fails_WhenPublisherIsNull()
    {
        var command = CorrectAddGameProfileCommand;
        command.Publisher = null;
        
        var sut = GetSut();

        await Assert.ThrowsAsync<InvalidParameterException>(() => sut.AddGameProfile(command));
    }
    
    private const string SomeGameProfileName = "Some Name";
    private const string SomeDescription = "Some description";
    private const string SomeGenre = "FPS";
    private const string SomeDeveloper = "Some developer";
    private const string SomePublisher = "Some publisher";

    private static Commands.AddGameProfile CorrectAddGameProfileCommand => new()
    {
        Name = SomeGameProfileName,
        Description = SomeDescription,
        Genre = SomeGenre,
        Developer = SomeDeveloper,
        Publisher = SomePublisher
    };

    private GameProfileService GetSut() => new(_gameProfileRepository, _mockTimeService);
}