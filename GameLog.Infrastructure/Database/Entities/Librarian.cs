namespace GameLog.Infrastructure.Database.Entities;

public class Librarian : DbEntity
{
    public string Email { get; set; }
    public string Nickname { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
