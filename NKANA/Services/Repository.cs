using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NKANA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NKANA.Services
{
    public class Repository<T, TModel> : IRepository<T, TModel> where TModel : class
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly DbSet<TModel> _dbSet;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TModel>();
        }

        public async Task DeleteAsync(T key)
        {
            var entity = await _dbSet.FindAsync(key);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public Task DeleteAsync(Func<TModel, bool> expression)
        {
            var entities = _dbSet.Where(expression);
            foreach (var entity in entities)
            {
                _dbSet.Remove(entity);
            }
            return Task.CompletedTask;
        }

        public void DeleteRange(IEnumerable<TModel> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public IQueryable<TModel> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public async Task InsertAsync(TModel model)
        {
            await _dbSet.AddAsync(model);
        }

        public async Task InsertOrUpdate(TModel model)
        {
            var entity = _dbContext.Entry(model);
            if (entity != null)
            {
                Update(model);
            }
            else
            {
                await InsertAsync(model);
            }
        }

        public async Task InsertRangeAsync(IEnumerable<TModel> model)
        {
            await _dbSet.AddRangeAsync(model);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public DbTransactionInfo SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();

                return new DbTransactionInfo { Succeeded = true };
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new DbTransactionInfo
                {
                    Errors = new List<string> { ex.Message },
                    Succeeded = false
                };
            }
            catch (DbUpdateException ex)
            {
                return new DbTransactionInfo
                {
                    Errors = new List<string> { ex.Message },
                    Succeeded = false
                };
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public async Task<DbTransactionInfo> SaveChangesAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();

                return new DbTransactionInfo { Succeeded = true };
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new DbTransactionInfo
                {
                    Errors = new List<string> { ex.Message },
                    Succeeded = false
                };
            }
            catch (DbUpdateException ex)
            {
                return new DbTransactionInfo
                {
                    Errors = new List<string> { ex.Message },
                    Succeeded = false
                };
            }
        }

        public void Update(TModel model)
        {
            _dbSet.Update(model);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }
    }
}
