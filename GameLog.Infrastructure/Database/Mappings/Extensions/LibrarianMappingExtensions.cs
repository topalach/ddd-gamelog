using AutoMapper;
using GameLog.Domain.Common;
using GameLog.Domain.Librarians;

namespace GameLog.Infrastructure.Database.Mappings.Extensions;

public static class LibrarianMappingExtensions
{
    public static void ConfigureLibrarianMappings(this IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<Entities.Librarian, Librarian>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => new FullName(src.FirstName, src.LastName)))
            .ReverseMap()
            .ForPath(x => x.FirstName, opt => opt.MapFrom(src => src.FullName.FirstName))
            .ForPath(x => x.LastName, opt => opt.MapFrom(src => src.FullName.LastName));
    }
}