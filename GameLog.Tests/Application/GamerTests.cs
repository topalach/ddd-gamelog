// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using GameLog.Common.Gamers;
// using GameLog.Domain.Actors;
// using GameLog.Domain.Common;
// using GameLog.Domain.GameProfiles;
// using GameLog.Domain.PlayedGames;
// using GameLog.Tests.Utils.Repositories;
// using Xunit;
//
// namespace GameLog.Tests.Application;
//
// public class GamerTests
// {
//     private readonly IGamerRepository _gamerRepository;
//     private readonly IPlayedGameRepository _playedGameRepository;
//     private readonly IGamerCommandHandler _gamerCommandHandler;
//
//     public GamerTests()
//     {
//          _gamerRepository = new InMemoryGamerRepository();
//          _playedGameRepository = new InMemoryPlayedGameRepository();
//          
//          _gamerCommandHandler = new GamerCommandHandler(_gamerRepository);
//     }
//     
//     [Fact]
//     public async Task SetsInitialGamerRank_ToRookie()
//     {
//         var gamer = CreateGamer();
//         await _gamerRepository.Add(gamer);
//         
//         _gamerCommandHandler.Handle(
//             new Commands.Gamers.UpdateGamerRank
//             {
//                 GamerId = DefaultGamerId.Value
//             });
//
//         gamer = _gamerRepository.Load(DefaultGamerId);
//
//         Assert.Equal(GamerRank.Casual, gamer.Rank);
//     }
//     
//     [Theory]
//     [InlineData(4, GamerRank.Rookie)]
//     [InlineData(5, GamerRank.Regular)]
//     [InlineData(6, GamerRank.Regular)]
//     [InlineData(19, GamerRank.Regular)]
//     [InlineData(20, GamerRank.Hardcore)]
//     [InlineData(21, GamerRank.Hardcore)]
//     public async Task SetsGamerRank_WhenGamesPlayed(int numberOfGamesPlayed, GamerRank expectedGamerRank)
//     {
//         var gamer = CreateGamer();
//         await _gamerRepository.Add(gamer);
//
//         for (var i = 0; i < numberOfGamesPlayed; i++)
//             await CreatePlayedGame(i, DefaultGamerId);
//
//         _gamerCommandHandler.Handle(
//             new Commands.Gamers.UpdateGamerRank
//             {
//                 GamerId = DefaultGamerId.Value
//             });
//
//         gamer = _gamerRepository.Load(DefaultGamerId);
//
//         Assert.Equal(expectedGamerRank, gamer.Rank);
//     }
//
//     private static readonly GamerId DefaultGamerId = new("88443a04-47f1-49b3-b94e-b7a6df2d3dc2");
//     private static readonly GameProfileId DefaultGameProfileId = new("a6eee12d-2352-47be-b786-793d59d83a0e");
//
//     private static Gamer CreateGamer() => new(
//         DefaultGamerId,
//         new Email("john@example.com"),
//         new Nickname("john"),
//         new NonEmptyDateTime(DateTimeOffset.UtcNow),
//         new List<PlayedGameId>());
//
//     private Task CreatePlayedGame(int index, GamerId gamerId)
//     {
//         var playedGameId = new PlayedGameId($"played-game-{index}");
//         var createdAt = new NonEmptyDateTime(DateTimeOffset.UtcNow);
//         
//         return _playedGameRepository.Add(
//             new PlayedGame(
//                 playedGameId,
//                 gamerId,
//                 DefaultGameProfileId,
//                 createdAt));
//     }
// }