using GameLog.Domain.Common;

namespace GameLog.Domain.Gamers;

public static class Events
{
    public abstract class GamerEvent : Event
    {
    }

    public class GamerCreated : GamerEvent
    {
        public override string Name() => nameof(GamerCreated);

        public GamerId Id { get; set; }
        public Email Email { get; set; }
        public Nickname Nickname { get; set; }
        public NonEmptyDateTime CreatedAt { get; set; }
    }

    public class FullNameUpdated : GamerEvent
    {
        public override string Name() => nameof(FullNameUpdated);

        public FullName? FullName { get; set; }
    }

    public class UpdateNumberOfPlayedGames : GamerEvent
    {
        public override string Name() => nameof(UpdateNumberOfPlayedGames);
        
        public NumberOfPlayedGames NumberOfPlayedGames { get; set; }
    }
}