﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManager.Persistence.Domain.Models.Entities;

[Table("Users")]
public class User : AbstractDatedRecord
{
    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public required string Email { get; set; }

    [Required]
    [PasswordPropertyText]
    [MaxLength(255)]
    public required string Password { get; set; }
    
    [Required]
    [EnumDataType(typeof(Role))]
    public Role Role { get; set; } = Role.User;

    // Relations
    [ForeignKey("UserId")]
    public List<Password> Passwords { get; init; } = null!;
}