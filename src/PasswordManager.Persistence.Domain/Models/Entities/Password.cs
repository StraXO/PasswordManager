using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManager.Persistence.Domain.Models.Entities;

[Table("UserPasswords")]
public class Password : AbstractDatedRecord
{
    [MinLength(4)] [MaxLength(255)] public string Username { get; set; } = null!;

    [Required]
    [PasswordPropertyText]
    [MaxLength(255)]
    public required string HashedPassword { get; set; } = null!;

    [Required]
    [PasswordPropertyText]
    [MaxLength(255)]
    public required string Salt { get; set; } = null!;

    [Required] [Url] [MaxLength(255)] public string WebsiteUrl { get; set; } = null!;

    [MaxLength(255)] public string Notes { get; set; } = null!;

    // Relations
    [ForeignKey("UserId")] public long UserId { get; set; }
}