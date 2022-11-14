using System;
using GameLog.Domain.Common;
using GameLog.Domain.GameProfiles;
using GameLog.Domain.Gamers;
using GameLog.Domain.PlayedGames;
using Xunit;

namespace GameLog.Tests.Domain;

public class PlayedGameTests
{
    [Fact]
    public void SetsRequiredFields_OnCreate()
    {
        var playedGame = PlayedGame.Create(
            SomePlayedGameId,
            SomeOwnerGamerId,
            SomeGameProfileId,
            SomeCreatedAtDate);
        
        Assert.NotNull(playedGame);
        
        Assert.Equal(SomePlayedGameId, playedGame.Id);
        Assert.Equal(SomeOwnerGamerId, playedGame.OwnerGamerId);
        Assert.Equal(SomeGameProfileId, playedGame.GameProfileId);
        Assert.Equal(SomeCreatedAtDate, playedGame.CreatedAt);
        
        Assert.Equal(NumberOfHoursPlayed.Zero, playedGame.HoursPlayed);
        Assert.Null(playedGame.Status);
        Assert.Null(playedGame.Score);
    }

    [Fact]
    public void UpdatesHoursPlayed()
    {
        const int hoursPlayed = 3;
        
        var playedGame = CreatePlayedGame();
        Assert.Equal(NumberOfHoursPlayed.Zero, playedGame.HoursPlayed);

        playedGame.UpdateHoursPlayed(new NumberOfHoursPlayed(hoursPlayed));
        
        Assert.Equal(new NumberOfHoursPlayed(hoursPlayed), playedGame.HoursPlayed);
    }

    [Fact]
    public void DoesNotSetGameStatus_WhenUpdatesNumberOfHoursPlayed_AndNoHoursYet()
    {
        const int hoursPlayed = 3;
        
        var playedGame = CreatePlayedGame();

        playedGame.UpdateHoursPlayed(new NumberOfHoursPlayed(hoursPlayed));
        
        Assert.Null(playedGame.Status);
    }

    [Fact]
    public void UpdatesScore()
    {
        var score = GameScore.From0To10(7);
        
        var playedGame = CreatePlayedGame();
        
        playedGame.UpdateScore(score);
        
        Assert.Equal(GameScore.From0To10(7), playedGame.Score);
    }

    private static PlayedGame CreatePlayedGame()
        => PlayedGame.Create(
            SomePlayedGameId,
            SomeOwnerGamerId,
            SomeGameProfileId,
            SomeCreatedAtDate);

    private static PlayedGameId SomePlayedGameId => new("f024730d-a68e-4bef-b736-60eb605a1068");
    private static GamerId SomeOwnerGamerId => new("9a5f53c0-0240-4889-a5cd-b28d7a3a7f45");
    private static GameProfileId SomeGameProfileId => new("719330ed-ba1f-47de-995a-1aaa1b05c884");
    private static NonEmptyDateTime SomeCreatedAtDate => new(new DateTimeOffset(2022, 11, 13, 0, 0, 0, TimeSpan.Zero));
}