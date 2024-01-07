namespace PasswordManager.Persistence.Domain.Models.Response;

public class PasswordResponse
{
    public string Username { get; set; } = null!;

    public required string Password { get; set; } = null!;

    public string WebsiteUrl { get; set; } = null!;

    public string Notes { get; set; } = null!;
}