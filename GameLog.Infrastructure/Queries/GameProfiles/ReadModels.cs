namespace GameLog.Infrastructure.Queries.GameProfiles;

public static class ReadModels
{
    public class GuestListItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public double AveragePercentageScore { get; set; }
        public double AverageHoursPlayed { get; set; }
    }
}