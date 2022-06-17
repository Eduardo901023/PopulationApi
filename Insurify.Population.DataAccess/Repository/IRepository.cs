using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Insurify.Population.DataAccess.DbContext;

namespace Insurify.Population.DataAccess.Repository
{
    public interface IRepository<TEntity, TKey>
    {
        IObjectContext Context { get; }
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);      
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Get a collection of TEntity
        /// </summary>
        /// <param name="skip">Number of rows to skip</param>
        /// <param name="take">Number of rows to take starting after skip rows</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync(int skip, int take);

        /// <summary>
        /// Find a TEntity by its Id
        /// </summary>
        /// <param name="id">TEntity's Id</param>
        /// <returns>Returns the TEntity that match with Id</returns>
        Task<TEntity> FindAsync(TKey id);

        Task SaveChangesAsync();
    }
}
