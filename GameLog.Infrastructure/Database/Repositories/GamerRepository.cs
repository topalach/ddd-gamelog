using AutoMapper;
using GameLog.Application.Gamers;
using GameLog.Domain.Gamers;
using Microsoft.EntityFrameworkCore;

namespace GameLog.Infrastructure.Database.Repositories;

public class GamerRepository : EntityFrameworkRepository, IGamerRepository
{
    public GamerRepository(GameLogDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public Task<GamerId> GetIdAsync() => GenerateId(id => new GamerId(id));

    public async Task StoreAsync(Gamer gamer)
    {
        var dbEntity = Map<Entities.Gamer>(gamer);
        await DbContext.Gamers.AddAsync(dbEntity);
    }

    public async Task<Gamer> LoadAsync(GamerId id)
    {
        var dbEntity = await DbContext.Gamers.FindAsync(id.Value);
        return Map<Gamer>(dbEntity);
    }
    
    public Task<bool> ExistsAsync(GamerId id) => DbContext.Gamers.AnyAsync(x => x.Id == id.Value);
    public Task<bool> ExistsByEmailAsync(string email) => DbContext.Gamers.AnyAsync(x => x.Email == email);
    public Task<bool> ExistsByNicknameAsync(string nickname) => DbContext.Gamers.AnyAsync(x => x.Nickname == nickname);
}