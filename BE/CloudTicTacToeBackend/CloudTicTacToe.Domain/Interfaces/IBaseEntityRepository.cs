using CloudTicTacToe.Domain.Models;
using System.Linq.Expressions;

namespace CloudTicTacToe.Domain.Interfaces
{
    public interface IBaseEntityRepository<TEntity> where TEntity : BaseDomainModel
    {
        TEntity GetById(Guid id, string includes = "");

        IEnumerable<TEntity> GetAll(string includes = "");

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>>? predicate = null, string includes = "");

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);
    }
}
