namespace GameLog.Application.Utils;

public interface ITimeService
{
    DateTimeOffset UtcNow();
}