namespace GameLog.Application.GameProfiles;

public static class Commands
{
    public class AddGameProfile
    {
        public string Name { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
    }
}