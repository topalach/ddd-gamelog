using GameLog.Domain.Common;

namespace GameLog.Domain.Actors;

public class Gamer
{
    public GamerId Id { get; }
    public Email Email { get; }
    public Nickname Nickname { get; }
    public FullName? FullName { get; private set; }
    public NonEmptyDateTime CreatedAt { get; }
    
    public Gamer(GamerId id, Email email, Nickname nickname, NonEmptyDateTime createdAt)
    {
        Id = id;
        Email = email;
        Nickname = nickname;
        CreatedAt = createdAt;
    }

    public void UpdateFullName(FullName? fullName)
    {
        FullName = fullName;
    }
}

public record GamerId : Id
{
    public GamerId(string id) : base(id)
    {
    }
}