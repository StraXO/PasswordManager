using System.Collections.Generic;
using System.Threading.Tasks;
using PasswordManager.API.Core.Identity;
using PasswordManager.Persistence.Domain.Models.Entities;
using PasswordManager.Persistence.Repositories;

namespace PasswordManager.API.Core.Services.Implementation;

public class PasswordService(IPasswordRepository repository, IClaimService claimService) : IPasswordService
{
    public async Task<Password> AddPasswordAsync(Password password)
    {
        password.UserId = claimService.GetUserId();

        return await repository.AddAsync(password);
    }

    public async Task<Password?> GetPasswordAsync(long id)
    {
        var password = await repository.FindAsync(password => password.Id == id);
        
        // Check if the password exists, if not return null
        if (password == null)
        {
            return null;
        }
        
        // Check if the user is the owner of the password, if not return null
        if (password.UserId != claimService.GetUserId())
        {
            return null;
        }
        
        return await repository.FindAsync(id);
    }

    public async Task<IEnumerable<Password>> GetUserPasswordsAsync()
    {
        return await repository.FindAllAsync(password => password.UserId == claimService.GetUserId());
    }

    public async Task<Password?> UpdatePasswordAsync(Password password)
    {
        if (password.UserId != claimService.GetUserId())
        {
            return null;
        }
        
        return await repository.UpdateAsync(password);
    }

    public async Task<bool> DeletePasswordAsync(long id)
    {
        return await repository.RemoveAsync(password => password.Id == id && password.UserId == claimService.GetUserId());
    }
}