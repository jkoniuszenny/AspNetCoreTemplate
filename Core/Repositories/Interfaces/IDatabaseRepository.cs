using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Repositories.Interfaces
{
    public interface IDatabaseRepository : IRepository
    {
        Task<T> SelectOne<T>(Expression<Func<T, bool>> filters, params System.Linq.Expressions.Expression<Func<T, object>>[] includes) where T : class;
        Task<IEnumerable<T>> SelectList<T>(Expression<Func<T, bool>> filters, params System.Linq.Expressions.Expression<Func<T, object>>[] includes) where T : class;
        Task Insert(IEnumerable<object> entities);
        Task Update(IEnumerable<object> entities);
        Task<T> SelectOneForTasks<T>(Expression<Func<T, bool>> filters, params Expression<Func<T, object>>[] includes) where T : class;
        Task<IEnumerable<T>> SelectListForTasks<T>(Expression<Func<T, bool>> filters, params Expression<Func<T, object>>[] includes) where T : class;
    }
}
