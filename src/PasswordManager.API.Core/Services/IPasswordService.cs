using System.Collections.Generic;
using System.Threading.Tasks;
using PasswordManager.Persistence.Domain.Models.Entities;

namespace PasswordManager.API.Core.Services;

public interface IPasswordService
{
    Task<Password> AddPasswordAsync(Password password);

    Task<Password?> GetPasswordAsync(int id);

    Task<IEnumerable<Password>> GetUserPasswordsAsync();

    Task<Password> UpdatePasswordAsync(Password password);

    Task<bool> DeletePasswordAsync(int id);
}