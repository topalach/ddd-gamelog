namespace GameLog.Infrastructure.Database.Entities;

public abstract class DbEntity
{
    public string Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}