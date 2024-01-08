using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PasswordManager.Persistence.Domain.Attributes;

namespace PasswordManager.Persistence.Domain.Models;

public abstract class AbstractDatedRecord : AbstractRecord
{
    [Required]
    [UpdateTimestamp(true, false, true)]
    public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;

    [Required]
    [UpdateTimestamp(true, true, true)]
    public DateTime ModifiedDateTime { get; set; } = DateTime.UtcNow;
}