using GameLog.Domain.Common;
using GameLog.Domain.Common.Exceptions;

namespace GameLog.Domain.Librarians;

public class Librarian : Aggregate<Events.LibrarianEvent>
{
    public LibrarianId Id { get; }
    public Email Email { get; }
    public Nickname Nickname { get; }
    public FullName? FullName { get; }
    public NonEmptyDateTime CreatedAt { get; }

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
        
        librarian.Apply(new Events.LibrarianCreated
        {
            Id = id,
            Email = email,
            Nickname = nickname,
            FullName = fullName,
            CreatedAt = createdAt
        });

        return librarian;
    }

    protected override void When(Events.LibrarianEvent @event)
    {
        //TODO: add aggregate tests
        //TODO: add domain service + tests
        //TODO: add UpdateFullName
        
        switch (@event)
        {
            case Events.LibrarianEvent e:
                Id = e.Id;
                Email = e.Email;
                Nickname = e.Nickname;
                FullName = e.FullName;
                CreatedAt = e.CreatedAt;
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