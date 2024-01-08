using Microsoft.EntityFrameworkCore;
using PasswordManager.Persistence.Domain.Models.Entities;

namespace PasswordManager.Persistence.PostgreSql;

/// <summary>
///     The database context for the application that allows communication with the database.
/// </summary>
/// <param name="context">The database context</param>
public class PostgresDbContext(DbContextOptions<PostgresDbContext> context) : DbContext(context)
{
    /// <summary>
    ///     A <see cref="DbSet{TEntity}" /> that represents the <see cref="User"/> table/record collection.
    ///     Can be used directly to query the database.
    /// </summary>
    public DbSet<User> Users { get; init; } = null!;

    /// <summary>
    ///     A <see cref="DbSet{TEntity}" /> that represents the <see cref="Password"/> table/record collection.
    ///     Can be used directly to query the database.
    /// </summary>
    public DbSet<Password> Passwords { get; init; } = null!;
}