using System.Collections.Generic;
using System.Threading.Tasks;
using PasswordManager.Persistence.Domain.Models.Entities;
using PasswordManager.Persistence.Repositories;

namespace PasswordManager.API.Core.Services.Implementation;

public class PasswordService(IPasswordRepository repository) : IPasswordService
{
    public async Task<Password> AddPasswordAsync(Password password)
    {
        return await repository.AddAsync(password);
    }

    public async Task<Password?> GetPasswordAsync(long id)
    {
        return await repository.FindAsync(id);
    }

    public async Task<IEnumerable<Password>> GetUserPasswordsAsync()
    {
        return await repository.FindAllAsync();
    }

    public async Task<Password> UpdatePasswordAsync(Password password)
    {
        return await repository.UpdateAsync(password);
    }

    public async Task<bool> DeletePasswordAsync(long id)
    {
        return await repository.RemoveAsync(id);
    }
}