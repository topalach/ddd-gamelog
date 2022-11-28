using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameLog.Common.Gamers;
using GameLog.Common.Validation;

namespace GameLog.Infrastructure.Database.Entities;

public class Gamer : DbEntity
{
    [MaxLength(ValidationConstants.MaxLength.Email)]
    public string Email { get; set; }
    
    [MaxLength(ValidationConstants.MaxLength.Nickname)]
    public string Nickname { get; set; }
    
    [MaxLength(ValidationConstants.MaxLength.FirstName)]
    public string FirstName { get; set; }
    
    [MaxLength(ValidationConstants.MaxLength.LastName)]
    public string LastName { get; set; }
    
    [Column(TypeName = "nvarchar(16)")]
    public GamerRank Rank { get; set; }
    
    public int NumberOfPlayedGames { get; set; }
    
    public List<PlayedGame> PlayedGames { get; set; }
}
