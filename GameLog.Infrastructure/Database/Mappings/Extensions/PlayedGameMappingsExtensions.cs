using AutoMapper;
using GameLog.Domain.Gamers;
using GameLog.Domain.PlayedGames;

namespace GameLog.Infrastructure.Database.Mappings.Extensions;

public static class PlayedGameMappingsExtensions
{
    public static void ConfigurePlayedGameMappings(this IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<Entities.PlayedGame, PlayedGame>()
            .ForMember(dest => dest.OwnerGamerId, opt => opt.MapFrom(src => new GamerId(src.GamerId)))
            .ForMember(dest => dest.Score, opt => opt.MapFrom(src => GameScore.From0To100(src.PercentageScore)))
            .ReverseMap()
            .ForPath(x => x.GamerId, opt => opt.MapFrom(src => src.OwnerGamerId.Value))
            .ForPath(x => x.PercentageScore, opt => opt.MapFrom(src => src.Score.Percentage));

        cfg.CreateMap<NumberOfHoursPlayed, int>().ConvertUsing(src => src.Value);
    }
}