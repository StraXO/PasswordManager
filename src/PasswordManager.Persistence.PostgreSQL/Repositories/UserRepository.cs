using Microsoft.EntityFrameworkCore;
using PasswordManager.Persistence.Domain.Models.Entities;
using PasswordManager.Persistence.Repositories;

namespace PasswordManager.Persistence.PostgreSql.Repositories;

public class UserRepository(PostgresDbContext context) : Repository<User>(context), IUserRepository;