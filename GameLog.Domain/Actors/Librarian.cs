using GameLog.Domain.Common;

namespace GameLog.Domain.Actors;

public class Librarian
{
    public LibrarianId Id { get; }
    public Email Email { get; }
    public Nickname Nickname { get; }
    public FullName? FullName { get; }
    public NonEmptyDateTime CreatedAt { get; }

    public Librarian(LibrarianId id, Email email, Nickname nickname, FullName fullName, NonEmptyDateTime createdAt)
    {
        Id = id;
        Email = email;
        Nickname = nickname;
        FullName = fullName;
        CreatedAt = createdAt;
    }
}

public record LibrarianId : Id
{
    public LibrarianId(string id) : base(id)
    {
    }
}