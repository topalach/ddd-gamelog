#pragma warning disable CS8618
namespace GameLog.Application.Gamers;

public static class Commands
{
    public class AddGamer
    {
        public string Email { get; set; }
        public string Nickname { get; set; }
    }

    public class UpdateFullName
    {
        public string GamerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UpdateNumberOfPlayedGames
    {
        public string GamerId { get; set; }
    }
}