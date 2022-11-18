using GameLog.Application.Utils;
using GameLog.Domain.Common;
using GameLog.Domain.Librarians;

namespace GameLog.Application.Librarians;

public class LibrarianService
{
    private readonly ILibrarianRepository _librarianRepository;
    private readonly ITimeService _timeService;

    public LibrarianService(ILibrarianRepository librarianRepository, ITimeService timeService)
    {
        _librarianRepository = librarianRepository;
        _timeService = timeService;
    }

    public async Task<string> AddLibrarian(Commands.AddLibrarian command)
    {
        var emailTaken = await _librarianRepository.ExistsByEmailAsync(new Email(command.Email));

        if (emailTaken)
            throw new InvalidOperationException($"Librarian with email '{command.Email}' was already added.");

        var nicknameTaken = await _librarianRepository.ExistsByNicknameAsync(new Nickname(command.Nickname));
        
        if (nicknameTaken)
            throw new InvalidOperationException($"Librarian with nickname '{command.Nickname}' was already added.");
        
        //TODO: check gamer emails and nicknames as well

        var id = await _librarianRepository.GetIdAsync();
        var createdAt = new NonEmptyDateTime(_timeService.UtcNow());

        var librarian = Librarian.Create(
            id,
            new Email(command.Email),
            new Nickname(command.Nickname),
            new FullName(command.FirstName, command.LastName),
            createdAt);

        await _librarianRepository.StoreAsync(librarian);
        return id.Value;
    }

    public async Task UpdateFullName(Commands.UpdateFullName command)
    {
        var librarian = await LoadLibrarian(command.LibrarianId);
        
        var fullName = new FullName(command.FirstName, command.LastName);
        librarian.UpdateFullName(fullName);

        await _librarianRepository.SaveChangesAsync();
    }

    private async Task<Librarian> LoadLibrarian(string id)
    {
        var gamer = await _librarianRepository.LoadAsync(new LibrarianId(id));

        if (gamer is null)
            throw new InvalidOperationException($"Could not find a librarian with ID {id}.");

        return gamer;
    }
}