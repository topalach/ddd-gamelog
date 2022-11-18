using GameLog.Application.Utils;
using GameLog.Domain.Common;
using GameLog.Domain.Librarians;

namespace GameLog.Application.Librarians;

public interface ILibrarianRepository : IRepository<LibrarianId, Librarian>
{
    Task<bool> ExistsByEmailAsync(Email email);
    Task<bool> ExistsByNicknameAsync(Nickname nickname);
}