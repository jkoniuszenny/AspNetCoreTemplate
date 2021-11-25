using Core.Repositories.Interfaces;
using Infrastructure.Database;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using Infrastructure.Extensions;

namespace Infrastructure.Repositories
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly DatabaseSettings _settings;

        public DatabaseRepository(DatabaseContext databaseContext, DatabaseSettings settings)
        {
            _databaseContext = databaseContext;
            _settings = settings;
        }

        /// <summary>
        ///  Insert dla sporej liczby danych, typu Bulk, pod spodem tmp table i merge
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task InsertBulk<T>(List<T> entities) where T : class
        {
            try
            {
                await _databaseContext.BulkInsertAsync(entities);
                await _databaseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException("Dane nie zostały zapisane. Ktoś w międzyczasie wykonał ich zmianę. Odśwież i spróbuj ponownie", ex);
            }
            finally
            {
                Parallel.ForEach(entities, p =>
                {
                    _databaseContext.Entry(p).State = EntityState.Detached;
                });
            }
        }

        /// <summary>
        /// Insert normalny
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task Insert(IEnumerable<object> entities)
        {
            try
            {
                await _databaseContext.AddRangeAsync(entities);
                await _databaseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException("Dane nie zostały zapisane. Ktoś w międzyczasie wykonał ich zmianę. Odśwież i spróbuj ponownie", ex);
            }
            finally
            {
                Parallel.ForEach(entities, p =>
                {
                    _databaseContext.Entry(p).State = EntityState.Detached;
                });
            }
        }

        /// <summary>
        /// Update dla sporej liczby danych, typu Bulk, pod spodem tmp table i merge
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task UpdateBulk<T>(List<T> entities) where T : class
        {
            try
            {
                await _databaseContext.BulkUpdateAsync(entities);
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

        /// <summary>
        /// Update normalny
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task Update(List<object> entities)
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

        /// <summary>
        /// Pobranie jednego rekordu - do użycia w przypadku standardowego wykorzystania
        /// </summary>
        /// <typeparam name="T">Model tabeli</typeparam>
        /// <param name="filters">Warunek do wywołania na modelu przy pobraniu danych</param>
        /// <param name="includes">Dołącenie tabel z innych modeli (FK)</param>
        /// <returns></returns>
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

        /// <summary>
        /// Pobranie listy rekordów - do użycia w przypadku standardowego wykorzystania
        /// </summary>
        /// <typeparam name="T">Model tabeli</typeparam>
        /// <param name="filters">Warunek do wywołania na modelu przy pobraniu danych</param>
        /// <param name="includes">Dołącenie tabel z innych modeli (FK)</param>
        /// <returns></returns>
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

        /// <summary>
        /// Pobranie jednego rekordu - do użycia w przypadku korzystania z List<Task>>
        /// </summary>
        /// <typeparam name="T">Model tabeli</typeparam>
        /// <param name="filters">Warunek do wywołania na modelu przy pobraniu danych</param>
        /// <param name="includes">Dołącenie tabel z innych modeli (FK)</param>
        /// <returns></returns>
        public async Task<T> SelectOneForTasks<T>(Expression<Func<T, bool>> filters, params Expression<Func<T, object>>[] includes) where T : class
        {

            try
            {
                DatabaseContext context = new DatabaseContext(_databaseContext.Options, _settings);

                IQueryable<T> query = context.Set<T>();

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

        /// <summary>
        /// Pobranie listy rekordów - do użycia w przypadku korzystania z List<Task>>
        /// </summary>
        /// <typeparam name="T">Model tabeli</typeparam>
        /// <param name="filters">Warunek do wywołania na modelu przy pobraniu danych</param>
        /// <param name="includes">Dołącenie tabel z innych modeli (FK)</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> SelectListForTasks<T>(Expression<Func<T, bool>> filters, params Expression<Func<T, object>>[] includes) where T : class
        {
            try
            {
                DatabaseContext context = new DatabaseContext(_databaseContext.Options, _settings);

                IQueryable<T> query = context.Set<T>();

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
