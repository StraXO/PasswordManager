using PasswordManager.Persistence.Domain.Models.Entities;

namespace PasswordManager.Persistence.Repositories;

/// <summary>
///     The repository responsible for the <see cref="Password"/> table.
/// </summary>
public interface IPasswordRepository : IRepository<Password>;