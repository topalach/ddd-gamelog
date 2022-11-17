using AutoMapper;
using GameLog.Domain.Gamers;

namespace GameLog.Infrastructure.Database.Mappings.Extensions;

public static class GamerMappingExtensions
{
    public static void ConfigureGamerMappings(this IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<Entities.Gamer, Gamer>()
            .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src => new Domain.Common.FullName(src.FirstName, src.LastName)))
            .ReverseMap()
            .ForPath(x => x.FirstName, opt => opt.MapFrom(src => src.FullName.FirstName))
            .ForPath(x => x.LastName, opt => opt.MapFrom(src => src.FullName.LastName));

        cfg.CreateMap<string, GamerId>().ConvertUsing(src => new GamerId(src));

        cfg.CreateMap<int, NumberOfPlayedGames>().ConvertUsing(src => new NumberOfPlayedGames(src));
        cfg.CreateMap<NumberOfPlayedGames, int>().ConvertUsing(src => src.Value);
    }
}