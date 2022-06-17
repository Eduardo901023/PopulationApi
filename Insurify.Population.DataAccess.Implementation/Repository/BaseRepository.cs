using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Insurify.Population.DataAccess.DbContext;
using Insurify.Population.DataAccess.Repository;
using Microsoft.Data.SqlClient;

namespace Insurify.Population.DataAccess.Implementation.Repository
{
    public class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        public IObjectContext Context { get; }
        protected string TableName { get; }

        public BaseRepository(IObjectContext context, string tableName)
        {
            Context = context;
            TableName = tableName;
        }

        public Task AddAsync(TEntity entity)
        {
            return Task.Run(() =>
            {
                Context.Query<TEntity>().Add(entity);
            });
        }

        public Task DeleteAsync(TEntity entity)
        {
            return Task.Run(() =>
            {
                Context.Query<TEntity>().Remove(entity);
            });
        }

        public Task UpdateAsync(TEntity entity)
        {
            return Task.Run(() =>
            {
                Context.Query<TEntity>().Update(entity);
            });
        }

        public Task SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        protected IDbConnection GetConnection()
        {
            return new SqlConnection(Context.ConnectionString);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(int skip, int take)
        {
            var query = $@"SELECT * FROM {TableName}
                           ORDER BY Id
                           OFFSET {skip} ROWS
                           FETCH NEXT {take} ROWS ONLY";

            using (var sqlConnection = GetConnection())
            {
                var result = await sqlConnection.QueryAsync<TEntity>(query).ConfigureAwait(false);
                return result;
            }
        }

        public virtual async Task<TEntity> FindAsync(TKey id)
        {
            var query = $"SELECT TOP 1 * FROM {TableName} WHERE Id = '{id}'";

            using (var sqlConnection = GetConnection())
            {
                var result = await sqlConnection.QueryAsync<TEntity>(query).ConfigureAwait(false);
                return result.SingleOrDefault();
            }
        }
    }
}
