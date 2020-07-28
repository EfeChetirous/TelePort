using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Teleport.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, IObjectState
    {
        private readonly IDataContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IHttpContextAccessor _httpContext;

        //private readonly IUnitOfWork _unitOfWork;
        public Repository(IDataContext context, IHttpContextAccessor _httpContext)
        {
            _context = context;
            this._httpContext = _httpContext;
            //_unitOfWork = unitOfWork;
            var dbContext = context as DbContext;

            if (dbContext != null)
            {
                _dbSet = dbContext.Set<TEntity>();
            }
        }
        public virtual void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            entity.ObjectState = ObjectState.Deleted;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
            _context.SaveChanges();
        }

        public virtual TEntity Find(params object[] keyValues)
        {
            var query = _dbSet.Find(keyValues);
            return query;
        }
        public IQueryable<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            var query = _dbSet.AsQueryable();
            var retValue = query.Where(predicate);
            return retValue;
        }
        public System.Linq.IQueryable<TEntity> GetAll()
        {
            var query = _dbSet.AsQueryable();
            return query;
        }
        public void Insert(TEntity entity)
        {
         
            entity.ObjectState = ObjectState.Added;

            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
            _context.SaveChanges();
        }
        public void UpdateSingleValue(TEntity entity)
        {

        }

        public System.Linq.IQueryable<TEntity> Queryable()
        {
            return _dbSet;
        }

        public void Update(TEntity entity)
        {
          
            entity.ObjectState = ObjectState.Modified;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
            _context.SaveChanges();
        }

        public string GetUserName()
        {
            var userName = _httpContext.HttpContext.User?.Identity?.Name;
            return userName;
        }


        #region Async

        public virtual void DeleteById(int id)
        {
            var entity = _dbSet.Find(id);
            entity.ObjectState = ObjectState.Deleted;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
            _context.SaveChanges();
        }

        public virtual async Task DeleteByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            entity.ObjectState = ObjectState.Deleted;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            entity.ObjectState = ObjectState.Deleted;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            try
            {             
                entity.ObjectState = ObjectState.Modified;
                _dbSet.Attach(entity);
                _context.SyncObjectState(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Inner Ex :" + ex.InnerException?.Message);
            }

        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            try
            {
             
                entity.ObjectState = ObjectState.Added;
                _dbSet.Attach(entity);
                _context.SyncObjectState(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Inner Ex :" + ex.InnerException?.Message);
            }
        }
        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }

        public virtual async Task<TEntity> FindByExpression(Expression<Func<TEntity, int>> wheres)
        {
            return await _dbSet.FindAsync(wheres);
        }
        #endregion 


    }


    public static class RepoExtension
    {
        public static bool HasProperty(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName) != null;
        }
    }

}
