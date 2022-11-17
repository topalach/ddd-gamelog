using AutoMapper;
using GameLog.Domain.GameProfiles;

namespace GameLog.Infrastructure.Database.Mappings.Extensions;

public static class GameProfileMappingExtensions
{
    public static void ConfigureGameProfileMappings(this IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<Entities.GameProfile, GameProfile>()
            .ForMember(
                dest => dest.DevelopmentInfo,
                opt => opt.MapFrom(src => new DevelopmentInfo(src.Developer, src.Publisher)))
            .ReverseMap()
            .ForPath(x => x.Developer, opt => opt.MapFrom(src => src.DevelopmentInfo.Developer))
            .ForPath(x => x.Publisher, opt => opt.MapFrom(src => src.DevelopmentInfo.Publisher));

        cfg.CreateMap<GameName, string>().ConvertUsing(src => src.Value);
        cfg.CreateMap<GameProfileDescription, string>().ConvertUsing(src => src.Value);
        cfg.CreateMap<Genre, string>().ConvertUsing(src => src.Value);
    }
}