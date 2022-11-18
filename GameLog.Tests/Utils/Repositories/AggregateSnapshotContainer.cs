using System.Collections.Generic;
using System.Text.Json;

namespace GameLog.Tests.Utils.Repositories;

internal class AggregateSnapshotContainer
{
    private readonly Dictionary<string, HashSet<AggregateSnapshot>> _snapshotsPerId = new();

    public void Add(string id, object aggregate)
    {
        if (_snapshotsPerId.ContainsKey(id) == false)
            _snapshotsPerId[id] = new HashSet<AggregateSnapshot>();

        var snapshot = AggregateSnapshot.Create(aggregate);
        _snapshotsPerId[id].Add(snapshot);
    }
    
    public int GetSnapshotCount(string id)
        => _snapshotsPerId.ContainsKey(id)
            ? _snapshotsPerId[id].Count
            : 0;
}

internal record AggregateSnapshot
{
    public string Json { get; }

    private AggregateSnapshot(string json)
    {
        Json = json;
    }

    public static AggregateSnapshot Create(object aggregate)
    {
        var json = JsonSerializer.Serialize(aggregate);
        return new AggregateSnapshot(json);
    }
}
