using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Persistence.Repositories;

namespace PasswordManager.Persistence.PostgreSql.Repositories;

public class Repository<T>(PostgresDbContext context) : IRepository<T> where T : class
{
    public async Task<T?> FindAsync(long id)
    {
        return await context.FindAsync<T>(id).ConfigureAwait(false);
    }

    public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await context.Set<T>().Where(predicate).FirstOrDefaultAsync().ConfigureAwait(false);
    }

    public async Task<IEnumerable<T>> FindAllAsync()
    {
        return await context.Set<T>().ToListAsync().ConfigureAwait(false);
    }

    public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
    {
        return await context.Set<T>().Where(predicate).ToListAsync().ConfigureAwait(false);
    }

    public async Task<bool> ExistsAsync(long id)
    {
        var result = await context.FindAsync<T>(id).ConfigureAwait(false);

        return result != null;
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return await context.Set<T>().Where(predicate).FirstOrDefaultAsync().ConfigureAwait(false) != null;
    }

    public async Task<T> AddAsync(T entity)
    {
        var result = await context.AddAsync(entity).AsTask().ConfigureAwait(false);

        await context.SaveChangesAsync().ConfigureAwait(false);

        return result.Entity;
    }

    public async Task AddManyAsync(IEnumerable<T> entities)
    {
        await context.AddRangeAsync(entities).ConfigureAwait(false);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var result = context.Update(entity);

        await context.SaveChangesAsync().ConfigureAwait(false);

        return result.Entity;
    }

    public async Task RemoveAsync(T entity)
    {
        context.Remove(entity);

        await context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        if (!await ExistsAsync(id))
            return false;

        var entity = await FindAsync(id).ConfigureAwait(false);
        await RemoveAsync(entity!).ConfigureAwait(false);

        return true;
    }

    public async Task<bool> RemoveAsync(Expression<Func<T, bool>> predicate)
    {
        if (!await ExistsAsync(predicate).ConfigureAwait(false))
            return false;

        var entity = await FindAsync(predicate).ConfigureAwait(false);
        await RemoveAsync(entity!).ConfigureAwait(false);

        return true;
    }

    public async Task RemoveRangeAsync(IEnumerable<T> entities)
    {
        context.RemoveRange(entities);

        await context.SaveChangesAsync().ConfigureAwait(false);
    }
}