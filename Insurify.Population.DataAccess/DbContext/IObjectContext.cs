using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Insurify.Population.DataAccess.DbContext
{
    public interface IObjectContext
    {
        string ConnectionString { get; }
        DbSet<TEntity> Query<TEntity>() where TEntity : class;
        Task SaveChangesAsync();
    }
}
