using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManager.Persistence.Domain.Models;

public abstract class AbstractDatedRecord : AbstractRecord
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedDateTime { get; init; } = DateTime.UtcNow;
}