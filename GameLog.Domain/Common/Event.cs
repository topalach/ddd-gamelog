namespace GameLog.Domain.Common;

public abstract class Event
{
    public abstract string Name();
    
    public DateTimeOffset CreatedAt { get; }

    public Event()
    {
        CreatedAt = DateTimeOffset.UtcNow;
    }
}