using GameLog.Common.Gamers;
using GameLog.Domain.Common;
using GameLog.Domain.Common.Exceptions;

namespace GameLog.Domain.Gamers;

public class Gamer : Aggregate<GamerId, Events.GamerEvent>
{
    public Email Email { get; private set; }
    public Nickname Nickname { get; private set; }
    public FullName? FullName { get; private set; }
    public NonEmptyDateTime CreatedAt { get; private set; }
    public GamerRank Rank { get; private set; }
    
    public NumberOfPlayedGames NumberOfPlayedGames { get; private set; }

#pragma warning disable CS8618
    private Gamer()
#pragma warning restore CS8618
    {
    }

    public static Gamer Create(
        GamerId id,
        Email email,
        Nickname nickname,
        NonEmptyDateTime createdAt)
    {
        var gamer = new Gamer();
        
        gamer.Apply(new Events.GamerCreated
        {
            Id = id,
            Email = email,
            Nickname = nickname,
            When = createdAt
        });

        return gamer;
    }

    public void UpdateFullName(FullName? fullName)
    {
        Apply(new Events.FullNameUpdated
        {
            FullName = fullName
        });
    }

    public void UpdateNumberOfPlayedGames(NumberOfPlayedGames numberOfPlayedGames)
    {
        Apply(new Events.UpdateNumberOfPlayedGames
        {
            NumberOfPlayedGames = numberOfPlayedGames
        });
    }

    protected override void When(Events.GamerEvent @event)
    {
        switch (@event)
        {
            case Events.GamerCreated e:
                Id = e.Id;
                Email = e.Email;
                Nickname = e.Nickname;
                CreatedAt = e.When;
                NumberOfPlayedGames = NumberOfPlayedGames.Zero;
                break;
            
            case Events.FullNameUpdated e:
                FullName = e.FullName;
                break;
            
            case Events.UpdateNumberOfPlayedGames e:
                NumberOfPlayedGames = e.NumberOfPlayedGames;
                Rank = CalculateRank(e.NumberOfPlayedGames);
                break;
                
            default:
                throw new UnrecognizedEventException(@event);
        }
    }

    private static GamerRank CalculateRank(NumberOfPlayedGames numberOfPlayedGames)
        => numberOfPlayedGames switch
        {
            _ when numberOfPlayedGames < 5 => GamerRank.Rookie,
            _ when numberOfPlayedGames < 10 => GamerRank.Casual,
            _ when numberOfPlayedGames < 20 => GamerRank.Regular,
            _ => GamerRank.Hardcore
        };
    
    //TODO: override GetStateValidationErrors to ensure the aggregate is valid
}

public record GamerId : Id
{
    public GamerId(string id) : base(id)
    {
    }
}

public record NumberOfPlayedGames : IComparable<NumberOfPlayedGames>, IComparable<int>
{
    public int Value { get; }

    public NumberOfPlayedGames(int value)
    {
        if (value < 0)
            throw new InvalidParameterException("Number of games played cannot be lower than 0", nameof(value));
        
        Value = value;
    }

    public static NumberOfPlayedGames Zero => new(0);
    
    public int CompareTo(NumberOfPlayedGames? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return Value.CompareTo(other.Value);
    }
    
    public int CompareTo(int other)
    {
        return Value.CompareTo(other);
    }

    public static bool operator >(NumberOfPlayedGames x, int y) => x.Value > y;
    public static bool operator <(NumberOfPlayedGames x, int y) => x.Value < y;
}
