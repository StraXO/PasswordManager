using Microsoft.EntityFrameworkCore;
using PasswordManager.Persistence.Domain.Models.Entities;
using PasswordManager.Persistence.Repositories;

namespace PasswordManager.Persistence.PostgreSql.Repositories;

public class PasswordRepository(PostgresDbContext context) : Repository<Password>(context), IPasswordRepository;