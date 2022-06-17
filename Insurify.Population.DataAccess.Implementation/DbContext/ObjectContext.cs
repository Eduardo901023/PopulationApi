using System.Threading.Tasks;
using Insurify.Population.Business.Domain;
using Insurify.Population.DataAccess.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Insurify.Population.DataAccess.Implementation.DbContext
{
    public class ObjectContext : Microsoft.EntityFrameworkCore.DbContext, IObjectContext
    {
        public string ConnectionString { get; }

        public ObjectContext(DbContextOptions<ObjectContext> options, IConfiguration configuration) : base(options)
        {
            ConnectionString = configuration.GetConnectionString("CountryPopulationDb");
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryPopulation> CountryPopulations { get; set; }

        public DbSet<TEntity> Query<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public Task SaveChangesAsync()
        {
            return SaveChangesAsync();
        }
    }
}
