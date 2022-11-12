using GameLog.Domain.Common;

namespace GameLog.Domain.GameProfiles;

public static class Events
{
    public abstract class GameProfileEvent : Event
    {
    }
    
    public class GameProfileCreated : GameProfileEvent
    {
        public override string Name() => nameof(GameProfileCreated);

        public GameProfileId Id { get; set; }
        public GameName GameProfileName { get; set; }
        public Genre Genre { get; set; }
        public DevelopmentInfo DevelopmentInfo { get; set; }
        public GameProfileDescription Description { get; set; }
        public NonEmptyDateTime GameProfileCreatedAt { get; set; }
    }
}