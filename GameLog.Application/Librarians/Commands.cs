#pragma warning disable CS8618
namespace GameLog.Application.Librarians;

public static class Commands
{
    public class AddLibrarian
    {
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UpdateFullName
    {
        public string LibrarianId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}