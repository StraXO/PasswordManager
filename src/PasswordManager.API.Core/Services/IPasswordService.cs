using System.Collections.Generic;
using System.Threading.Tasks;
using PasswordManager.Persistence.Domain.Models.Entities;
using PasswordManager.Persistence.Repositories;

namespace PasswordManager.API.Core.Services;

/// <summary>
///     Service responsible of processing logic for the <see cref="IPasswordRepository"/>
/// </summary>
public interface IPasswordService
{
    /// <summary>
    ///     Add a password.
    /// </summary>
    /// <param name="password">The password to add.</param>
    /// <returns>
    ///     A task that represents the asynchronous add operation.
    ///     Returns a <see cref="Password"/>.
    /// </returns>
    Task<Password> AddPasswordAsync(Password password);

    /// <summary>
    ///     Find a password by id.
    /// </summary>
    /// <param name="id">The password id.</param>
    /// <returns>
    ///     A task that represents the asynchronous get operation.
    ///     Returns a <see cref="Password"/> or null.
    /// </returns>
    Task<Password?> FindPasswordAsync(long id);

    /// <summary>
    ///     Find all passwords for the logged in user.
    /// </summary>
    /// <returns>
    ///     A task that represents the asynchronous get operation.
    ///     Returns a <see cref="IEnumerable{T}"/> collection of <see cref="Password"/>.
    /// </returns>
    Task<IEnumerable<Password>> FindUserPasswordsAsync();

    /// <summary>
    ///     Update a password.
    /// </summary>
    /// <param name="password">The password to update.</param>
    /// <returns>
    ///     A task that represents the asynchronous update operation.
    ///     Returns a <see cref="Password"/> or null.
    /// </returns>
    Task<Password?> UpdatePasswordAsync(Password password);

    /// <summary>
    ///     Delete a password by id.
    /// </summary>
    /// <param name="id">The id of the password to remove.</param>
    /// <returns>
    ///     A task that represents the asynchronous delete operation.
    ///     Returns a <see cref="bool"/> indicating whether the delete request succeeded.
    /// </returns>
    Task<bool> RemovePasswordAsync(long id);
}