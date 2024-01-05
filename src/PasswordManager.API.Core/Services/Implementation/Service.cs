using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PasswordManager.API.Core.Services.Implementation;

public class Service<T>(DbContext context) : IService<T> where T : class
{
    public async Task<T?> FindAsync(long id)
    {
        return await context.FindAsync<T>(id);
    }

    public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await context.FindAsync<T>(predicate);
    }

    public async Task<IEnumerable<T>> FindAllAsync()
    {
        return await context.Set<T>().ToListAsync();
    }
    
    public async Task<bool> ExistsAsync(long id)
    {
        var result = await context.FindAsync<T>(id).ConfigureAwait(false);

        return result != null;
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        var result = await context.FindAsync<T>(predicate);

        return result != null;
    }

    public async Task<T> AddAsync(T entity)
    {
        var result = await context.AddAsync(entity).AsTask();

        await context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var result = context.Update(entity);
        
        await context.SaveChangesAsync();
        
        return result.Entity;
    }

    public async Task RemoveAsync(T entity)
    {
        context.Remove(entity);

        await context.SaveChangesAsync();
    }

    public async Task<bool> RemoveAsync(long id)
    {
        if (!ExistsAsync(id).Result)
            return false;

        var entity = FindAsync(id).Result;
        await RemoveAsync(entity!);
        
        return true;
    }

    public async Task<bool> RemoveAsync(Expression<Func<T, bool>> predicate)
    {
        if (!ExistsAsync(predicate).Result)
            return false;

        var entity = FindAsync(predicate).Result;
        await RemoveAsync(entity!);
        
        return true;
    }
}