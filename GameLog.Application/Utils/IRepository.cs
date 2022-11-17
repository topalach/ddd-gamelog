using GameLog.Domain.Common;

namespace GameLog.Application.Utils;

public interface IRepository<TAggregateId, TAggregate> where TAggregateId : Id
{
    Task<TAggregateId> GetIdAsync();
    Task StoreAsync(TAggregate gameProfile);
    Task<TAggregate?> LoadAsync(TAggregateId id);
    Task SaveChangesAsync();
}