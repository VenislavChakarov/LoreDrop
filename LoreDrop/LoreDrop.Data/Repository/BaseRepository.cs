using System.Linq.Expressions;
using System.Reflection;
using LoreDrop.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LoreDrop.Data.Repository;

public class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey>,
    IAsyncRepository<TEntity, TKey>
    where TEntity : class
{
    protected readonly LoreDropDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;
    
public BaseRepository(LoreDropDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }
    
    public TEntity? GetById(TKey id)
    {
        return this._dbSet
            .Find(id);
    }

    public TEntity? SingleOrDefault(Func<TEntity, bool> predicate)
    {
        return this._dbSet
            .SingleOrDefault(predicate);
    }

    public TEntity? FirstOrDefault(Func<TEntity, bool> predicate)
    {
        return this._dbSet
            .FirstOrDefault(predicate);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return this._dbSet
            .ToArray();
    }

    public int Count()
    {
        return this._dbSet
            .Count();
    }

    public IQueryable<TEntity> GetAllAttached()
    {
        return this._dbSet
            .AsQueryable();
    }

    public void Add(TEntity item)
    { 
        this._dbSet.Add(item);
        this._context.SaveChanges();
    }

    public void AddRange(IEnumerable<TEntity> items)
    {
        this._dbSet.AddRange(items);
        this._context.SaveChanges();
    }

    public bool Delete(TEntity entity)
    {
        this.PerformSoftDeleteOfEntity(entity);

        return this.Update(entity);
    }

    public bool HardDelete(TEntity entity)
    {
        this._dbSet.Remove(entity);
        int updatedCount = this._context.SaveChanges();
        
        return updatedCount > 0;
    }

    public bool Update(TEntity item)
    {
        try
        {
            this._dbSet.Attach(item);
            this._dbSet.Entry(item).State = EntityState.Modified;
            this._context.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public void SaveChanges()
    {
        this._context.SaveChanges();
    }

    public ValueTask<TEntity?> GetByIdAsync(TKey id)
    {
        return this._dbSet
            .FindAsync(id);
    }

    public Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return this._dbSet
            .SingleOrDefaultAsync(predicate);
    }

    public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return this._dbSet
            .FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        TEntity[] entities = await this._dbSet
            .ToArrayAsync();
            
        return entities;
    }

    public Task<int> CountAsync()
    {
        return this._dbSet
            .CountAsync();
    }

    public async Task AddAsync(TEntity item)
    {
        await this._dbSet.AddAsync(item);
        await  this._context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> items)
    {
        await this._dbSet.AddRangeAsync(items);
        await this._context.SaveChangesAsync();
    }

    public Task<bool> DeleteAsync(TEntity entity)
    {
        this.PerformSoftDeleteOfEntity(entity);
        
        return this.UpdateAsync(entity);
    }

    public async Task<bool> HardDeleteAsync(TEntity entity)
    {
        this._dbSet.Remove(entity);
        int updateCnt = await this._context.SaveChangesAsync();

        return updateCnt > 0;
    }

    public async  Task<bool> UpdateAsync(TEntity item)
    {
        try
        {
            this._dbSet.Attach(item);
            this._dbSet.Entry(item).State = EntityState.Modified;
            await this._context.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task SaveChangesAsync()
    {
        await this._context.SaveChangesAsync();
    }
    
    
    private void PerformSoftDeleteOfEntity(TEntity entity)
    {
        PropertyInfo? isDeletedProperty = 
            this.GetIsDeletedProperty(entity);
        if (isDeletedProperty == null)
        {
            throw new InvalidOperationException("Soft delete is not supported for this entity type. ");
        }

        isDeletedProperty.SetValue(entity, true);
    }

    private PropertyInfo? GetIsDeletedProperty(TEntity entity)
    {
        return typeof(TEntity)
            .GetProperties()
            .FirstOrDefault(pi => pi.PropertyType == typeof(bool) &&
                                  pi.Name == "IsDeleted");
    }
}