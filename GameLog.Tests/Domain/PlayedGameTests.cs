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
    }
    
    private static PlayedGameId SomePlayedGameId => new("f024730d-a68e-4bef-b736-60eb605a1068");
    private static GamerId SomeOwnerGamerId => new("9a5f53c0-0240-4889-a5cd-b28d7a3a7f45");
    private static GameProfileId SomeGameProfileId => new("719330ed-ba1f-47de-995a-1aaa1b05c884");
    private static NonEmptyDateTime SomeCreatedAtDate => new(new DateTimeOffset(2022, 11, 13, 0, 0, 0, TimeSpan.Zero));
}