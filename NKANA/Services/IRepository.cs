using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NKANA.Services
{
    public interface IRepository<T, TModel>
    {
        IQueryable<TModel> AsQueryable();

        IDbContextTransaction BeginTransaction();

        Task InsertAsync(TModel model);

        Task InsertOrUpdate(TModel model);

        Task InsertRangeAsync(IEnumerable<TModel> model);

        Task DeleteAsync(T key);

        Task DeleteAsync(Func<TModel, bool> expression);

        void DeleteRange(IEnumerable<TModel> entities);

        void Update(TModel model);

        /// <summary>
        /// Saves all database changes made to the backing store
        /// </summary>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        /// <exception cref="DbUpdateException"></exception>
        /// <returns></returns>
        Task<DbTransactionInfo> SaveChangesAsync();

        /// <summary>
        /// Saves all database changes made to the backing store
        /// </summary>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        /// <exception cref="DbUpdateException"></exception>
        /// <returns></returns>
        DbTransactionInfo SaveChanges();
    }
}
