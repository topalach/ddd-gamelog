using AutoMapper;
using GameLog.Infrastructure.Database.Mappings.Extensions;

namespace GameLog.Infrastructure.Database.Mappings;

public static class GameLogDbMappingFactory
{
    public static MapperConfiguration GetConfiguration()
        => new(
            cfg =>
            {
                cfg.ConfigureCommonMappings();
                cfg.ConfigureGamerMappings();
                cfg.ConfigureLibrarianMappings();
                cfg.ConfigureGameProfileMappings();
                cfg.ConfigurePlayedGameMappings();
            });
    
    public static IMapper CreateMapper() => GetConfiguration().CreateMapper();
}