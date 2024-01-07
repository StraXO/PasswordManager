using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Persistence.Domain.Models.Requests;

public class AuthRequest
{
    [Required]
    [EmailAddress]
    [MaxLength(255, ErrorMessage = "Email must be less than 255 characters long")]
    public string Email { get; init; } = null!;

    [Required]
    [PasswordPropertyText]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    [MaxLength(255, ErrorMessage = "Password must be less than 255 characters long")]
    public string Password { get; init; } = null!;
}