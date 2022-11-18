using GameLog.Domain.Common;
using GameLog.Domain.Common.Exceptions;

namespace GameLog.Domain.Librarians;

public class Librarian : Aggregate<LibrarianId, Events.LibrarianEvent>
{
    public Email Email { get; private set; }
    public Nickname Nickname { get; private set; }
    public FullName FullName { get; private set; }
    public NonEmptyDateTime CreatedAt { get; private set; }

#pragma warning disable CS8618
    private Librarian()
#pragma warning restore CS8618
    {
    }

    public static Librarian Create(
        LibrarianId id,
        Email email,
        Nickname nickname,
        FullName fullName,
        NonEmptyDateTime createdAt)
    {
        var librarian = new Librarian();

        librarian.Apply(
            new Events.LibrarianCreated
            {
                Id = id,
                Email = email,
                Nickname = nickname,
                FullName = fullName,
                LibrarianCreatedAt = createdAt
            });

        return librarian;
    }
    
    public void UpdateFullName(FullName fullName)
    {
        Apply(new Events.FullNameUpdated
        {
            FullName = fullName
        });
    }

    protected override void When(Events.LibrarianEvent @event)
    {
        switch (@event)
        {
            case Events.LibrarianCreated e:
                Id = e.Id;
                Email = e.Email;
                Nickname = e.Nickname;
                FullName = e.FullName;
                CreatedAt = e.LibrarianCreatedAt;
                break;
            
            case Events.FullNameUpdated e:
                FullName = e.FullName;
                break;
            
            default:
                throw new UnrecognizedEventException(@event);
        }
    }
    
    //TODO: override GetStateValidationErrors to ensure the aggregate is valid
}

public record LibrarianId : Id
{
    public LibrarianId(string id) : base(id)
    {
    }
}