using System.Collections.Generic;

namespace GameLog.Tests.Utils.Repositories;

public abstract class InMemoryRepository<T>
{
    protected readonly List<T> Items = new();

    protected string GetNextId() => (Items.Count + 1).ToString();
}