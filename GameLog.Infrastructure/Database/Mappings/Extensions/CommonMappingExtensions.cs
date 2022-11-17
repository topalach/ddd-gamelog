using AutoMapper;
using GameLog.Domain.Common;

namespace GameLog.Infrastructure.Database.Mappings.Extensions;

public static class CommonMappingExtensions
{
    public static void ConfigureCommonMappings(this IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<Id, string>().ConvertUsing(src => src.Value);
        
        cfg.CreateMap<NonEmptyDateTime, DateTimeOffset>().ConvertUsing(src => src.Value);
        cfg.CreateMap<DateTimeOffset, NonEmptyDateTime>().ConvertUsing(src => new NonEmptyDateTime(src));
        
        cfg.CreateMap<Nickname, string>().ConvertUsing(src => src.Value);
        cfg.CreateMap<Email, string>().ConvertUsing(src => src.Value);
    }
}