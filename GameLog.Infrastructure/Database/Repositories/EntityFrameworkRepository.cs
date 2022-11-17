using AutoMapper;

namespace GameLog.Infrastructure.Database.Repositories;

public abstract class EntityFrameworkRepository
{
    protected readonly GameLogDbContext DbContext;
    private readonly IMapper _mapper;

    protected EntityFrameworkRepository(GameLogDbContext dbContext, IMapper mapper)
    {
        DbContext = dbContext;
        _mapper = mapper;
    }

    protected static Task<TId> GenerateId<TId>(Func<string, TId> createId)
    {
        var id = Guid.NewGuid().ToString();
        return Task.FromResult(createId(id));
    }

    public Task SaveChangesAsync()
    {
        return DbContext.SaveChangesAsync();
    }

    protected TDestination Map<TDestination>(object source)
        => _mapper.Map<TDestination>(source);
}