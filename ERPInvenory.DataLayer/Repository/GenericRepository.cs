using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;
using ERPInventory.Model.Models;
using System.Data.Entity.Infrastructure;
namespace ERPInventory.DataLayer.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal ERPInventoryDBContext context;
        internal DbSet<T> dbSet;

        public GenericRepository(ERPInventoryDBContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> Get( Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = dbSet.AsQueryable();
            foreach (var property in includeProperties)
                query = query.Include(property);
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderBy != null)
            {
                return orderBy(query).AsEnumerable();
            }
            return query.AsEnumerable();
        }

  
        public virtual T GetById(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = dbSet.AsQueryable();
            foreach (var property in includeProperties)
                query = query.Include(property);
            return query.Where(predicate).FirstOrDefault();
        }

        public IEnumerable<T> SQLQuery(string sql, params object[] parameters)
        {
            return context.Database.SqlQuery<T>(sql, parameters).AsEnumerable();
        }

        public virtual void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
