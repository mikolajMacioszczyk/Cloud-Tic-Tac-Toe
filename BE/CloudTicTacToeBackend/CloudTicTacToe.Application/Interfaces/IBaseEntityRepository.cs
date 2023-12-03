using CloudTicTacToe.Domain.Models;
using System.Linq.Expressions;

namespace CloudTicTacToe.Application.Interfaces
{
    public interface IBaseEntityRepository<TEntity> where TEntity : BaseDomainModel
    {
        Task<TEntity?> GetByIDAsync(Guid id, string includeProperties = "");
        Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "");
        TEntity? GetFirstOrDefault(Expression<Func<TEntity, bool>> filter, string includeProperties = "");
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "");
        Task<IEnumerable<TEntity>> TakeAsync(int max, Expression<Func<TEntity, bool>>? filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "");
        Task<bool> Exist(Guid id);

        Task AddAsync(TEntity entity);
     
        void Update(TEntity entity);

        void Delete(Guid id);
        void Delete(TEntity entity);
    }
}
