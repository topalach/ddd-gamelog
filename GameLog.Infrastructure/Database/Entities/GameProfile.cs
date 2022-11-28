using System.ComponentModel.DataAnnotations;
using GameLog.Common.Validation;

namespace GameLog.Infrastructure.Database.Entities;

public class GameProfile : DbEntity
{
    [MaxLength(ValidationConstants.MaxLength.GameProfiles.Name)]
    public string Name { get; set; }
    
    [MaxLength(ValidationConstants.MaxLength.GameProfiles.Genre)]
    public string Genre { get; set; }
    
    [MaxLength(ValidationConstants.MaxLength.GameProfiles.Developer)]
    public string Developer { get; set; }
    
    [MaxLength(ValidationConstants.MaxLength.GameProfiles.Publisher)]
    public string Publisher { get; set; }
    
    [MaxLength(ValidationConstants.MaxLength.GameProfiles.Description)]
    public string Description { get; set; }
}
