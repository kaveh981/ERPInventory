using ERPInventory.Model.BindingModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERPInventory.DataLayer.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        PagedResult<T> Get(Expression<Func<T, bool>> predicate = null,
                            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Expression<Func<T, object>>[] includeProperties = null, int skip = 0, int take = 0);
        T GetById(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        IEnumerable<T> SQLQuery(string sql, params object[] parameters);
        void Insert(T entity);
        void Delete(object id);
        void Delete(T entity);
        void Update(T entity);
    }
}
