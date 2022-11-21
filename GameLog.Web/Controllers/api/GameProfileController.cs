using GameLog.Infrastructure.Database;
using GameLog.Infrastructure.Queries.GameProfiles;
using Microsoft.AspNetCore.Mvc;

namespace GameLog.Web.Controllers.api;

[ApiController]
[Route("api/game-profile")]
public class GameProfileController : ControllerBase
{
    private readonly GameLogDbContext _dbContext;

    public GameProfileController(GameLogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("guest-list/{pageNumber:int}/{pageSize:int?}")]
    public async Task<ActionResult<ReadModels.GuestListItem>> GetGuestGameProfiles(int pageNumber, int pageSize = 10)
    {
        var queryParams = new GameProfileQueries.GuestListQueryParams
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        
        var result = await GameProfileQueries.GetGuestList(_dbContext, queryParams);
        return Ok(result);
    }
}