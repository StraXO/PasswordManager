using PasswordManager.Persistence.Domain.Models.Entities;

namespace PasswordManager.Persistence.Repositories;

/// <summary>
///     The repository responsible for the <see cref="User"/> table.
/// </summary>
public interface IUserRepository : IRepository<User>;