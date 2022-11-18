using System.Linq;
using System.Threading.Tasks;
using GameLog.Application.Librarians;
using GameLog.Domain.Common;
using GameLog.Domain.Librarians;

namespace GameLog.Tests.Utils.Repositories;

public class InMemoryLibrarianRepository : InMemoryRepository<Librarian>, ILibrarianRepository
{
    public Task<LibrarianId> GetIdAsync()
    {
        var librarianId = new LibrarianId(GetNextId());
        return Task.FromResult(librarianId);
    }

    public Task StoreAsync(Librarian gameProfile)
    {
        Items.Add(gameProfile);
        return Task.CompletedTask;
    }

    public Task<Librarian?> LoadAsync(LibrarianId id)
    {
        var librarian = Items.SingleOrDefault(x => x.Id == id);
        return Task.FromResult(librarian);
    }

    public Task SaveChangesAsync()
    {
        return Task.CompletedTask;
    }

    public Task<bool> ExistsByEmailAsync(Email email)
    {
        var exists = Items.Any(x => x.Email == email);
        return Task.FromResult(exists);
    }

    public Task<bool> ExistsByNicknameAsync(Nickname nickname)
    {
        var exists = Items.Any(x => x.Nickname == nickname);
        return Task.FromResult(exists);
    }
}