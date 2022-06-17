using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Insurify.Population.DataAccess.Repository;
using Insurify.Population.Business.Contracts.ApplicationServices;

namespace Insurify.Population.Business.Implementation.ApplicationServices
{
    public class BaseApplicationService<TEntity, TKey> : IApplicationService<TEntity, TKey>
    {
        protected IRepository<TEntity, TKey> Repository { get; }

        public BaseApplicationService(IRepository<TEntity, TKey> repository)
        {
            Repository = repository;
        }

        public async Task AddAsync(TEntity entity)
        {
            await Repository.AddAsync(entity);
            await Repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Repository.DeleteAsync(entity);
            await Repository.SaveChangesAsync();
        }

        public Task<TEntity> FindAsync(TKey id)
        {
            return Repository.FindAsync(id);
        }

        public Task<IEnumerable<TEntity>> GetAllAsync(int skip, int take)
        {
            return Repository.GetAllAsync(skip, take);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Repository.UpdateAsync(entity);
            await Repository.SaveChangesAsync();
        }
    }
}
