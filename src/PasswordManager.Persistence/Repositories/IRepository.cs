using System.Linq.Expressions;

namespace PasswordManager.Persistence.Repositories;

public interface IRepository<T> where T : class
{
    /// <summary>
    ///     Find an entity by id.
    /// </summary>
    /// <param name="id">The id of the entity to find</param>
    /// <returns>
    ///     A task that represents the asynchronous get operation.
    ///     Returns the entity <see cref="T"/>.
    /// </returns>
    Task<T?> FindAsync(long id);

    /// <summary>
    ///     Find an entity by predicate.
    /// </summary>
    /// <param name="predicate">The <see cref="Expression" /> that will be used to look for the entity.</param>
    /// <returns>
    ///     A task that represents the asynchronous get operation.
    ///     Returns the entity <see cref="T"/>.
    /// </returns>
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    ///     Find all entities.
    /// </summary>
    /// <returns>
    ///     A task that represents the asynchronous get operation.
    ///     Returns a collection of entities <see cref="IEnumerable{T}"/>.
    /// </returns>
    Task<IEnumerable<T>> FindAllAsync();

    /// <summary>
    ///     Find all entities by predicate.
    /// </summary>
    /// <param name="predicate">The <see cref="Expression" /> that will be used to look for the entities.</param>
    /// <returns>
    ///     A task that represents the asynchronous get operation.
    ///     Returns a collection of entities <see cref="IEnumerable{T}"/>.
    /// </returns>
    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    ///     Check if an entity exists by id.
    /// </summary>
    /// <param name="id">The id of the entity to check</param>
    /// <returns>
    ///     A task that represents the asynchronous check operation.
    ///     Returns a <see cref="bool"/> indicating whether the entity exists.
    /// </returns>
    Task<bool> ExistsAsync(long id);

    /// <summary>
    ///     Check if an entity exists by id.
    /// </summary>
    /// <param name="predicate">The <see cref="Expression" /> that will be used to look for the entity.</param>
    /// <returns>
    ///     A task that represents the asynchronous check operation.
    ///     Returns a <see cref="bool"/> indicating whether the entity exists.
    /// </returns>
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    ///     Add a new entity.
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>
    ///     A task that represents the asynchronous add operation.
    ///     Returns the added entity <see cref="T"/>.
    /// </returns>
    Task<T> AddAsync(T entity);

    /// <summary>
    ///     Add a list of new entities.
    /// </summary>
    /// <param name="entities">The list of entities to add</param>
    /// <returns>
    ///     A task that represents the asynchronous add operation.
    /// </returns>
    Task AddManyAsync(IEnumerable<T> entities);

    /// <summary>
    ///     Update an entity.
    /// </summary>
    /// <param name="entity">The entity to update</param>
    /// <returns>
    ///     A task that represents the asynchronous update operation.
    ///     Returns the updated entity <see cref="T"/>.
    /// </returns>
    Task<T> UpdateAsync(T entity);

    /// <summary>
    ///     Delete an entity.
    ///  </summary>
    ///  <param name="entity">The entity to delete</param>
    ///  <returns>
    ///     A task that represents the asynchronous delete operation.
    ///  </returns>
    Task RemoveAsync(T entity);

    /// <summary>
    ///     Delete an entity by id.
    /// </summary>
    /// <param name="id">The id of the entity to delete</param>
    /// <returns>
    ///     A task that represents the asynchronous delete operation.
    /// </returns>
    Task<bool> RemoveAsync(long id);

    /// <summary>
    ///     Delete an entity by predicate.
    /// </summary>
    /// <param name="predicate">The <see cref="Expression" /> that will be used to look for the entity.</param>
    /// <returns>
    ///     A task that represents the asynchronous delete operation.
    /// </returns>
    Task<bool> RemoveAsync(Expression<Func<T, bool>> predicate);
}