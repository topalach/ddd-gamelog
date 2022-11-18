using GameLog.Domain.Common;
#pragma warning disable CS8618

namespace GameLog.Domain.Librarians;

public static class Events
{
    public abstract class LibrarianEvent : Event
    {
    }

    public class LibrarianCreated : LibrarianEvent
    {
        public override string Name() => nameof(LibrarianCreated);
        
        public LibrarianId Id { get; set; }
        public Email Email { get; set; }
        public Nickname Nickname { get; set; }
        public FullName FullName { get; set; }
        public NonEmptyDateTime LibrarianCreatedAt { get; set; }
    }

    public class FullNameUpdated : LibrarianEvent
    {
        public override string Name() => nameof(FullNameUpdated);
        public FullName FullName { get; set; }
    }
}