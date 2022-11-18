using System;
using System.Threading.Tasks;
using GameLog.Application.Librarians;
using GameLog.Application.Utils;
using GameLog.Domain.Common;
using GameLog.Domain.Common.Exceptions;
using GameLog.Domain.Librarians;
using GameLog.Tests.Common;
using GameLog.Tests.Mocks;
using GameLog.Tests.Utils.Repositories;
using Xunit;

namespace GameLog.Tests.Application;

public class LibrarianServiceTests
{
    private readonly InMemoryLibrarianRepository _librarianRepository = new();
    private readonly ITimeService _timeService = new MockTimeService();
    
    [Fact]
    public async Task CreatesLibrarian()
    {
        var command = GetCorrectAddCommand();
        
        var sut = GetSut();

        var id = await sut.AddLibrarian(command);

        var librarian = await _librarianRepository.LoadAsync(new LibrarianId(id));
        
        Assert.NotNull(librarian);

        var expectedEmail = new Email(command.Email);
        Assert.Equal(expectedEmail, librarian.Email);

        var expectedNickname = new Nickname(command.Nickname);
        Assert.Equal(expectedNickname, librarian.Nickname);

        var expectedFullName = new FullName(command.FirstName, command.LastName);
        Assert.Equal(expectedFullName, librarian.FullName);
        
        Assert.Equal(MockTimeService.DefaultUtcNow, librarian.CreatedAt.Value);
    }

    [Fact]
    public async Task CreateFails_WhenEmailAlreadyTaken()
    {
        const string email = "email@example.com";
        const string nickname1 = "nick1";
        const string nickname2 = "nick2";

        var command1 = GetCorrectAddCommand(email, nickname1);
        var command2 = GetCorrectAddCommand(email, nickname2);

        var sut = GetSut();

        await sut.AddLibrarian(command1);
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => sut.AddLibrarian(command2));
    }

    [Fact]
    public async Task CreateFails_WhenNicknameAlreadyTaken()
    {
        const string nickname = "nick";
        const string email1 = "email1@example.com";
        const string email2 = "email2@example.com";

        var command1 = GetCorrectAddCommand(email1, nickname);
        var command2 = GetCorrectAddCommand(email2, nickname);

        var sut = GetSut();

        await sut.AddLibrarian(command1);
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => sut.AddLibrarian(command2));
    }

    [Fact]
    public async Task CreateFails_WhenNullEmailProvided()
    {
        var command = GetCorrectAddCommand(email: null);

        var sut = GetSut();

        await Assert.ThrowsAsync<InvalidParameterException>(() => sut.AddLibrarian(command));
    }

    [Fact]
    public async Task CreateFails_WhenNullNicknameProvided()
    {
        var command = GetCorrectAddCommand(nickname: null);

        var sut = GetSut();

        await Assert.ThrowsAsync<InvalidParameterException>(() => sut.AddLibrarian(command));
    }

    [Fact]
    public async Task CreateFails_WhenNullFirstNameProvided()
    {
        var command = GetCorrectAddCommand();
        command.FirstName = null;

        var sut = GetSut();

        await Assert.ThrowsAsync<InvalidParameterException>(() => sut.AddLibrarian(command));
    }

    [Fact]
    public async Task CreateFails_WhenNullLastNameProvided()
    {
        var command = GetCorrectAddCommand();
        command.LastName = null;

        var sut = GetSut();

        await Assert.ThrowsAsync<InvalidParameterException>(() => sut.AddLibrarian(command));
    }

    [Fact]
    public async Task UpdateFullName_Succeeds()
    {
        var librarian = await StoreSomeLibrarian(firstName: "Tom", lastName: "Hardy");

        var sut = GetSut();

        await sut.UpdateFullName(
            new Commands.UpdateFullName
            {
                LibrarianId = librarian.Id.Value,
                FirstName = "John",
                LastName = "Doe"
            });
        
        Assert.Equal(new FullName("John", "Doe"), librarian.FullName);
        
        AssertSavedChanges(librarian.Id);
    }

    [Fact]
    public async Task UpdateFullName_Fails_WhenLibrarianDoesNotExist()
    {
        var sut = GetSut();

        var command = new Commands.UpdateFullName
        {
            LibrarianId = "librarian-id-does-not-exist",
            FirstName = "John",
            LastName = "Doe"
        };
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => sut.UpdateFullName(command));
        
        AssertEmptyRepository();
    }

    private static Commands.AddLibrarian GetCorrectAddCommand(
        string email = "librarian@example.com",
        string nickname = "librarian-nickname")
        => new()
        {
            Email = email,
            Nickname = nickname,
            FirstName = "John",
            LastName = "Doe"
        };

    private async Task<Librarian> StoreSomeLibrarian(string firstName, string lastName)
    {
        var id = await _librarianRepository.GetIdAsync();

        var librarian = Librarian.Create(
            id,
            new Email("email@example.com"),
            new Nickname("nick"),
            new FullName(firstName, lastName),
            TestData.SomeDomainTime);
        
        await _librarianRepository.StoreAsync(librarian);
        return librarian;
    }

    private LibrarianService GetSut() => new(_librarianRepository, _timeService);

    private void AssertSavedChanges(LibrarianId id) => _librarianRepository.AssertChangesApplied(id);

    private void AssertEmptyRepository() => _librarianRepository.AssertEmpty();
}