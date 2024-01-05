using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManager.Persistence.Domain.Models.Records;

[Table("Users")]
public class User : AbstractDatedRecord
{
    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public required string Email { get; init; }

    [Required]
    [PasswordPropertyText]
    [MaxLength(255)]
    public required string Password { get; set; }

    [PasswordPropertyText]
    [MaxLength(255)]
    public required string Salt { get; init; }

    // Relations
    [ForeignKey("UserId")]
    public List<PasswordRecord> Passwords { get; init; } = null!;
}