using GameLog.Domain.Common;
using GameLog.Domain.Exceptions;

namespace GameLog.Domain.Actors;

public class Gamer
{
    public GamerId Id { get; }
    public Nickname Nickname { get; }
    public Name FirstName { get; }
    public Name LastName { get; }
    public CreatedAt CreatedAt { get; }
    
    //TODO: add constructor
}

public record GamerId
{
    public string Id { get; }
    
    public GamerId(string id)
    {
        if (string.IsNullOrEmpty(id))
            throw new InvalidParameterException("Gamer ID cannot be null nor empty", nameof(id));
        
        Id = id;
    }
}