using System.Collections.Generic;
using System.Threading.Tasks;
using PasswordManager.Persistence.Domain.Models.Entities;

namespace PasswordManager.API.Core.Services;

public interface IPasswordService
{
    Task<Password> AddPasswordAsync(Password password);

    Task<Password?> GetPasswordAsync(long id);

    Task<IEnumerable<Password>> GetUserPasswordsAsync();

    Task<Password?> UpdatePasswordAsync(Password password);

    Task<bool> DeletePasswordAsync(long id);
}