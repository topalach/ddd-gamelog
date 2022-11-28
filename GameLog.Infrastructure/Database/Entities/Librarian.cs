using System.ComponentModel.DataAnnotations;
using GameLog.Common.Validation;

namespace GameLog.Infrastructure.Database.Entities;

public class Librarian : DbEntity
{
    [MaxLength(ValidationConstants.MaxLength.Email)]
    public string Email { get; set; }
    
    [MaxLength(ValidationConstants.MaxLength.Nickname)]
    public string Nickname { get; set; }
    
    [MaxLength(ValidationConstants.MaxLength.FirstName)]
    public string FirstName { get; set; }
    
    [MaxLength(ValidationConstants.MaxLength.LastName)]
    public string LastName { get; set; }
}
