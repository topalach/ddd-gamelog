 using System;
 using GameLog.Common.Gamers;
 using GameLog.Domain.Common;
 using GameLog.Domain.Gamers;
 using Xunit;

 namespace GameLog.Tests.Domain;

 public class GamerTests
 {
     private static GamerId SomeGamerId => new("a98ccefe-40ff-4384-8b12-2130dfb5f03e");
     private static Email SomeEmail => new("email@example.com");
     private static Nickname SomeNickname => new("someNickname");
     private static NonEmptyDateTime SomeCreatedAtDate => new(new DateTimeOffset(2022, 1, 14, 0, 0, 0, TimeSpan.Zero));

     [Fact]
     public void SetsRequiredFields_OnCreate()
     {
         var gamer = Gamer.Create(
             SomeGamerId,
             SomeEmail,
             SomeNickname,
             SomeCreatedAtDate);
         
         Assert.NotNull(gamer);
         
         Assert.Equal(SomeGamerId, gamer.Id);
         Assert.Equal(SomeEmail, gamer.Email);
         Assert.Equal(SomeNickname, gamer.Nickname);
         Assert.Equal(SomeCreatedAtDate, gamer.CreatedAt);
         
         Assert.Equal(GamerRank.Rookie, gamer.Rank);
         Assert.Equal(NumberOfPlayedGames.Zero, gamer.NumberOfPlayedGames);
     }

     [Theory]
     [InlineData(4, GamerRank.Rookie)]
     [InlineData(5, GamerRank.Casual)]
     [InlineData(6, GamerRank.Casual)]
     [InlineData(9, GamerRank.Casual)]
     [InlineData(10, GamerRank.Regular)]
     [InlineData(11, GamerRank.Regular)]
     [InlineData(19, GamerRank.Regular)]
     [InlineData(20, GamerRank.Hardcore)]
     [InlineData(21, GamerRank.Hardcore)]
     public void SetsGamerRank_WhenGamesPlayed(int numberOfGamesPlayed, GamerRank expectedGamerRank)
     {
         var gamer = CreateSomeGamer();

         gamer.UpdateNumberOfPlayedGames(new NumberOfPlayedGames(numberOfGamesPlayed));
         
         Assert.Equal(new NumberOfPlayedGames(numberOfGamesPlayed), gamer.NumberOfPlayedGames);
         Assert.Equal(expectedGamerRank, gamer.Rank);
     }

     [Fact]
     public void UpdatesFullName()
     {
         var gamer = CreateSomeGamer();
         
         Assert.Null(gamer.FullName);
         
         gamer.UpdateFullName(new FullName("First", "Last"));
         
         Assert.Equal(new FullName("First", "Last"), gamer.FullName);
     }

     private static Gamer CreateSomeGamer() => Gamer.Create(
         SomeGamerId,
         SomeEmail,
         SomeNickname,
         SomeCreatedAtDate);
 }