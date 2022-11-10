using GameLog.Common.Gamers;
using GameLog.Domain.Common;
using GameLog.Domain.PlayedGames;

namespace GameLog.Domain.Actors;

public class Gamer
{
    public GamerId Id { get; }
    public Email Email { get; }
    public Nickname Nickname { get; }
    public FullName? FullName { get; private set; }
    public NonEmptyDateTime CreatedAt { get; }
    public GamerRank Rank { get; }
    
    public List<PlayedGameId> PlayedGameIds { get; }

    public Gamer(
        GamerId id,
        Email email,
        Nickname nickname,
        NonEmptyDateTime createdAt,
        List<PlayedGameId> playedGameIds)
    {
        Id = id;
        Email = email;
        Nickname = nickname;
        CreatedAt = createdAt;
        PlayedGameIds = playedGameIds;
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
