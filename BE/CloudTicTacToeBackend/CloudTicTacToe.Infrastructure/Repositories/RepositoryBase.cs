using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CloudTicTacToe.Infrastructure.Repositories;
public class RepositoryBase<TEntity> : IBaseEntityRepository<TEntity>
    where TEntity : BaseDomainModel
{
    protected TicTacToeContext context;
    protected DbSet<TEntity> dbSet;

    public RepositoryBase(TicTacToeContext context)
    {
        this.context = context;
        dbSet = context.Set<TEntity>();
    }

    #region Create
    public virtual async Task AddAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await dbSet.AddAsync(entity);
    }

    #endregion

    #region Read

    public virtual async Task<TEntity?> GetByIDAsync(Guid id, string includeProperties = "")
    {
        IQueryable<TEntity> query = dbSet;

        foreach (var includeProperty in includeProperties.Split
         (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }

    public virtual async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
    {
        IQueryable<TEntity> query = dbSet;

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return await query.FirstOrDefaultAsync(filter);
    }

    public virtual TEntity? GetFirstOrDefault(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
    {
        IQueryable<TEntity> query = dbSet;

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return query.FirstOrDefault(filter);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "")
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }

    public async Task<IEnumerable<TEntity>> TakeAsync(int max, Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "")
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        return await query.Take(max).ToListAsync();
    }

    public virtual async Task<bool> Exist(Guid id)
    {
        return await dbSet.AnyAsync(x => x.Id == id);
    }

    #endregion

    #region Update
    public virtual void Update(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        dbSet.Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
    }
    #endregion

    #region Delete
    public virtual void Delete(Guid id)
    {
        TEntity? entity = dbSet.Find(id);

        if (entity is null)
        {
            throw new ArgumentNullException($"{nameof(entity)}, {id}");
        }

        Delete(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        if (context.Entry(entity).State == EntityState.Detached)
        {
            dbSet.Attach(entity);
        }
        dbSet.Remove(entity);
    }

    #endregion

}
