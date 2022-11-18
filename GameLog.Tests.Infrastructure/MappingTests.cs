using AutoMapper;
using GameLog.Common.Gamers;
using GameLog.Common.PlayedGames;
using GameLog.Domain.Librarians;
using GameLog.Infrastructure.Database.Mappings;
using GameLog.Tests.Common;
using Xunit;

using Entities = GameLog.Infrastructure.Database.Entities;

namespace GameLog.Tests.Infrastructure;

public class MappingTests
{
    private readonly MapperConfiguration _mapperConfiguration = GameLogDbMappingFactory.GetConfiguration();
    
    [Fact]
    public void MappingConfigurationIsValid()
    {
        _mapperConfiguration.AssertConfigurationIsValid();
    }

    [Fact]
    public void MapsGamer_Db_To_Domain()
    {
        var entity = new Entities.Gamer
        {
            Id = "2ace8657-0506-41cb-98cd-775c30c48c03",
            Email = "email@example.com",
            Nickname = "some-nickname",
            Rank = GamerRank.Hardcore,
            FirstName = "John",
            LastName = "Doe",
            NumberOfPlayedGames = 6,
            CreatedAt = TestData.SomeDateTimeOffset
        };

        var result = Act<Domain.Gamers.Gamer>(entity);
        
        Assert.NotNull(result);
        Assert.Equal(entity.Id, result.Id.Value);
        Assert.Equal(entity.Email, result.Email.Value);
        Assert.Equal(entity.Nickname, result.Nickname.Value);
        Assert.Equal(entity.Rank, result.Rank);
        Assert.Equal(entity.NumberOfPlayedGames, result.NumberOfPlayedGames.Value);
        Assert.Equal(entity.CreatedAt, result.CreatedAt.Value);
        
        Assert.NotNull(result.FullName);
        Assert.Equal(entity.FirstName, result.FullName.FirstName);
        Assert.Equal(entity.LastName, result.FullName.LastName);
    }

    [Fact]
    public void MapsGamer_Domain_To_Db()
    {
        var gamer = Domain.Gamers.Gamer.Create(
            new Domain.Gamers.GamerId("3d62f33b-388b-41ff-aa97-e76718f94be0"),
            new Domain.Common.Email("email@example.com"),
            new Domain.Common.Nickname("nickname"),
            TestData.SomeDomainTime);
        
        gamer.UpdateFullName(new Domain.Common.FullName("John", "Doe"));
        gamer.UpdateNumberOfPlayedGames(new Domain.Gamers.NumberOfPlayedGames(6));

        var result = Act<Entities.Gamer>(gamer);
        
        Assert.NotNull(result);
        Assert.Equal(gamer.Id.Value, result.Id);
        Assert.Equal(gamer.Email.Value, result.Email);
        Assert.Equal(gamer.Nickname.Value, result.Nickname);
        Assert.Equal(gamer.Rank, result.Rank);
        Assert.Equal(gamer.NumberOfPlayedGames.Value, result.NumberOfPlayedGames);
        Assert.Equal(gamer.CreatedAt.Value, result.CreatedAt);
        
        Assert.NotNull(gamer.FullName);
        Assert.Equal(gamer.FullName.FirstName, result.FirstName);
        Assert.Equal(gamer.FullName.LastName, result.LastName);
    }

    [Fact]
    public void MapsGameProfile_Db_To_Domain()
    {
        var entity = new Entities.GameProfile
        {
            Id = "e6cd0c0a-1064-4511-b985-27afdd9d73bc",
            Name = "Call of Battlefield",
            Description = "description",
            Developer = "ACME Inc.",
            Publisher = "PubCorp",
            Genre = "FPS",
            CreatedAt = TestData.SomeDateTimeOffset
        };

        var result = Act<Domain.GameProfiles.GameProfile>(entity);
        
        Assert.NotNull(result);
        
        Assert.Equal(entity.Id, result.Id.Value);
        Assert.Equal(entity.Name, result.Name.Value);
        Assert.Equal(entity.Description, result.Description.Value);
        Assert.Equal(entity.Developer, result.DevelopmentInfo.Developer);
        Assert.Equal(entity.Publisher, result.DevelopmentInfo.Publisher);
        Assert.Equal(entity.Genre, result.Genre.Value);
        Assert.Equal(entity.CreatedAt, result.CreatedAt.Value);
    }

    [Fact]
    public void MapsGameProfile_Domain_To_Db()
    {
        var gameProfile = Domain.GameProfiles.GameProfile.Create(
            new Domain.GameProfiles.GameProfileId("556f3a70-3e7e-4b21-a6ab-b1a1ebf30cc2"),
            new Domain.GameProfiles.GameName("God of Warcraft"),
            new Domain.GameProfiles.Genre("RPG"),
            new Domain.GameProfiles.DevelopmentInfo("GameDev Inc.", "GamePub Corp."),
            new Domain.GameProfiles.GameProfileDescription("game profile description"),
            TestData.SomeDomainTime);
        
        var result = Act<Entities.GameProfile>(gameProfile);
        
        Assert.NotNull(result);
        
        Assert.Equal(gameProfile.Id.Value, result.Id);
        Assert.Equal(gameProfile.Name.Value, result.Name);
        Assert.Equal(gameProfile.Description.Value, result.Description);
        Assert.Equal(gameProfile.DevelopmentInfo.Developer, result.Developer);
        Assert.Equal(gameProfile.DevelopmentInfo.Publisher, result.Publisher);
        Assert.Equal(gameProfile.Genre.Value, result.Genre);
        Assert.Equal(gameProfile.CreatedAt.Value, result.CreatedAt);
    }

    [Fact]
    public void MapsPlayedGame_Db_To_Domain()
    {
        var entity = new Entities.PlayedGame
        {
            Id = "e2a72868-b0b2-4772-ae8e-068de78b4e95",
            GamerId = "10e07f40-1ebf-42b1-a0e1-baa12fb5bb15",
            GameProfileId = "d5a690ce-6ac8-49b7-aa99-7b69721db8f1",
            HoursPlayed = 4,
            PercentageScore = 70,
            Status = PlayedGameStatus.Played,
            CreatedAt = TestData.SomeDateTimeOffset
        };

        var result = Act<Domain.PlayedGames.PlayedGame>(entity);
        
        Assert.NotNull(result);
        
        Assert.Equal(entity.Id, result.Id.Value);
        Assert.Equal(entity.GamerId, result.OwnerGamerId.Value);
        Assert.Equal(entity.GameProfileId, result.GameProfileId.Value);
        Assert.Equal(entity.HoursPlayed, result.HoursPlayed.Value);
        
        Assert.NotNull(result.Score);
        Assert.Equal(entity.PercentageScore, result.Score.Percentage);
        Assert.Equal(entity.Status, result.Status);
        Assert.Equal(entity.CreatedAt, result.CreatedAt.Value);
    }

    [Fact]
    public void MapsPlayedGame_Domain_To_Db()
    {
        var playedGame = Domain.PlayedGames.PlayedGame.Create(
            new Domain.PlayedGames.PlayedGameId("6ca9485e-7afb-49d0-a51c-8e392bd41cae"),
            new Domain.Gamers.GamerId("389f4498-f8a2-4aab-bc91-0d0a0ca7b5ce"),
            new Domain.GameProfiles.GameProfileId("9f393b8c-0295-4952-abb9-aca7178d54cd"),
            TestData.SomeDomainTime);
        
        playedGame.UpdateScore(Domain.PlayedGames.GameScore.From0To100(35));
        playedGame.UpdateHoursPlayed(new Domain.PlayedGames.NumberOfHoursPlayed(14));

        var result = Act<Entities.PlayedGame>(playedGame);
        
        Assert.NotNull(result);
        
        Assert.Equal(playedGame.Id.Value, result.Id);
        Assert.Equal(playedGame.OwnerGamerId.Value, result.GamerId);
        Assert.Equal(playedGame.GameProfileId.Value, result.GameProfileId);
        Assert.Equal(playedGame.HoursPlayed.Value, result.HoursPlayed);
        
        Assert.NotNull(playedGame.Score);
        Assert.Equal(playedGame.Score.Percentage, result.PercentageScore);
        Assert.Equal(playedGame.Status, result.Status);
        Assert.Equal(playedGame.CreatedAt.Value, result.CreatedAt);
    }

    [Fact]
    public void MapsLibrarian_Db_To_Domain()
    {
        var entity = new Entities.Librarian
        {
            Id = "ddce478f-6823-4011-933d-634d488b6441",
            Email = "librarian@example.com",
            Nickname = "libNick",
            FirstName = "Nick",
            LastName = "Doe",
            CreatedAt = TestData.SomeDateTimeOffset
        };

        var result = Act<Librarian>(entity);
        
        Assert.NotNull(result);
        
        Assert.Equal(entity.Id, result.Id.Value);
        Assert.Equal(entity.Email, result.Email.Value);
        Assert.Equal(entity.Nickname, result.Nickname.Value);
        
        Assert.NotNull(result.FullName);
        Assert.Equal(entity.FirstName, result.FullName.FirstName);
        Assert.Equal(entity.LastName, result.FullName.LastName);
        
        Assert.Equal(entity.CreatedAt, result.CreatedAt.Value);
    }

    [Fact]
    public void MapsLibrarian_Domain_To_Db()
    {
        throw new System.NotImplementedException("TODO");

        // var librarian = Librarian.Create()
    }

    private TDestination Act<TDestination>(object source)
    {
        var mapper = _mapperConfiguration.CreateMapper();
        return mapper.Map<TDestination>(source);
    }
}