using System;
using GameLog.Domain.Common;
using GameLog.Domain.GameProfiles;
using Xunit;

namespace GameLog.Tests.Domain;

public class GameProfileTests
{
    [Fact]
    public void SetsRequiredFields_OnCreate()
    {
        var gameProfile = GameProfile.Create(
            SomeGameProfileId,
            SomeGameName,
            SomeGenre,
            SomeDevelopmentInfo,
            SomeDescription,
            SomeCreatedAtDate);
        
        Assert.Equal(SomeGameProfileId, gameProfile.Id);
        Assert.Equal(SomeGameName, gameProfile.Name);
        Assert.Equal(SomeGenre, gameProfile.Genre);
        Assert.Equal(SomeDevelopmentInfo, gameProfile.DevelopmentInfo);
        Assert.Equal(SomeDescription, gameProfile.Description);
        Assert.Equal(SomeCreatedAtDate, gameProfile.CreatedAt);
    }
    
    private static GameProfileId SomeGameProfileId => new("f80ad11e-5b09-4126-a583-64dfce5a1c59");
    private static GameName SomeGameName => new("Some Game Name");
    private static Genre SomeGenre => new("RPG");
    private static DevelopmentInfo SomeDevelopmentInfo => new("Some Developer", "Some Publisher");
    private static GameProfileDescription SomeDescription => new("Some Game Profile description");
    private static NonEmptyDateTime SomeCreatedAtDate => new(new DateTimeOffset(2022, 05, 12, 0, 0, 0, TimeSpan.Zero));

}