using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Teleport.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : BaseEntity, IObjectState
    {
        void Insert(TEntity entity);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
        void DeleteById(int id);
        Task DeleteByIdAsync(int id);
        IQueryable<TEntity> Queryable();
        IQueryable<TEntity> GetAll();
        string GetUserName();
        TEntity Find(params object[] keyValues);
        IQueryable<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<TEntity> FindByExpression(Expression<Func<TEntity, int>> wheres);

    }
}
