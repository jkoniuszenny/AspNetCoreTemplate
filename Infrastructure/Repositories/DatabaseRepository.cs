using Core.Repositories.Interfaces;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly DatabaseContext _databaseContext;

        public DatabaseRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task Insert(IEnumerable<object> entities)
        {
            try
            {
                await _databaseContext.AddRangeAsync(entities);
                await _databaseContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                Parallel.ForEach(entities, p =>
                {
                    _databaseContext.Entry(p).State = EntityState.Detached;
                });
            }
        }

        public async Task Update(IEnumerable<object> entities)
        {
            try
            {
                _databaseContext.UpdateRange(entities);
                await _databaseContext.SaveChangesAsync();
            }
            catch
            {
            }
            finally
            {
                Parallel.ForEach(entities, p =>
                {
                    _databaseContext.Entry(p).State = EntityState.Detached;
                });
            }
        }

        public async Task<T> SelectOne<T>(Expression<Func<T, bool>> filters, params Expression<Func<T, object>>[] includes) where T : class
        {
            try
            {
                IQueryable<T> query = _databaseContext.Set<T>();

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                return await query
                    .Where(filters)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<T>> SelectList<T>(Expression<Func<T, bool>> filters, params Expression<Func<T, object>>[] includes) where T : class
        {
            try
            {
                IQueryable<T> query = _databaseContext.Set<T>();

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                return await query
                    .Where(filters)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch
            {
                return Enumerable.Empty<T>();
            }
        }

       
    }
}
