using GameLog.Domain.Common;
using GameLog.Domain.Librarians;
using GameLog.Tests.Common;
using Xunit;

namespace GameLog.Tests.Domain;

public class LibrarianTests
{
    [Fact]
    public void SetsRequiredFields_OnCreate()
    {
        var librarian = Librarian.Create(
            SomeLibrarianId,
            SomeEmail,
            SomeNickname,
            SomeFullName,
            SomeCreatedAtDate);
        
        Assert.NotNull(librarian);
        
        Assert.Equal(SomeLibrarianId, librarian.Id);
        Assert.Equal(SomeEmail, librarian.Email);
        Assert.Equal(SomeNickname, librarian.Nickname);
        Assert.Equal(SomeFullName, librarian.FullName);
        Assert.Equal(SomeCreatedAtDate, librarian.CreatedAt);
    }

    [Fact]
    public void UpdatesFullName()
    {
        var initialFullName = new FullName("Alex", "Smith");
        
        var librarian = Librarian.Create(
            SomeLibrarianId,
            SomeEmail,
            SomeNickname,
            initialFullName,
            SomeCreatedAtDate);
         
         librarian.UpdateFullName(new FullName("John", "Doe"));
         
         Assert.Equal(new FullName("John", "Doe"), librarian.FullName);
    }
    
    private static LibrarianId SomeLibrarianId => new("b4b599d4-6d02-45d2-9e9f-3823095f2b52");
    private static Email SomeEmail => new("some.email@example.com");
    private static Nickname SomeNickname => new("some-librarian-nickname");
    private static FullName SomeFullName => new("John", "Doe");
    private static NonEmptyDateTime SomeCreatedAtDate => TestData.SomeDomainTime;
}