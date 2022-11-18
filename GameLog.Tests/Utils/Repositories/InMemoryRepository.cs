using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameLog.Domain.Common;
using Xunit;

namespace GameLog.Tests.Utils.Repositories;

public abstract class InMemoryRepository<TId, TAggregate>
    where TAggregate : Aggregate<TId>
    where TId : Id
{
    protected readonly List<TAggregate> Items = new();

    private readonly AggregateSnapshotContainer _snapshots = new();

    protected string GetNextId() => (Items.Count + 1).ToString();

    public Task StoreAsync(TAggregate aggregate)
    {
        Items.Add(aggregate);
        AddSnapshot(aggregate);
        
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync()
    {
        foreach (var aggregate in Items)
            AddSnapshot(aggregate);
        
        return Task.CompletedTask;
    }

    public Task<TAggregate> LoadAsync(TId id)
    {
        var item = Items.SingleOrDefault(x => x.Id == id);
        return Task.FromResult(item);
    }

    private void AddSnapshot(TAggregate aggregate) => _snapshots.Add(aggregate.Id.Value, aggregate);

    public void AssertChangesApplied(TId id)
    {
        var snapshotCount = _snapshots.GetSnapshotCount(id.Value);
        
        Assert.True(
            snapshotCount > 1,
            $"Expected to have more than 1 snapshot of aggregate ID '{id.Value}' but have {snapshotCount}.");
    }

    public void AssertNoChangesApplied(TId id)
    {
        var snapshotCount = _snapshots.GetSnapshotCount(id.Value);
        Assert.Equal(1, snapshotCount);
    }

    public void AssertEmpty() => Assert.Empty(Items);
}