using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Insurify.Population.Business.Domain;
using Insurify.Population.DataAccess.DbContext;
using Insurify.Population.DataAccess.Implementation.Models;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            string fileName = "CountryPopulationData.json";
            string jsonString = File.ReadAllText(fileName);
            var data = JsonSerializer.Deserialize<IEnumerable<CountryPopulationJson>>(jsonString).GroupBy(p => p.CountryName);

            var countries = new List<Country>();
            var populations = new List<CountryPopulation>();
            foreach (var country in data)
            {
                countries.Add(new Country() { Name = country.Key});
                foreach (var population in country)
                {
                    countries[countries.Count - 1].Code = population.CountryCode;
                    populations.Add(new CountryPopulation() {
                        CountryId = countries[countries.Count - 1].Id,
                        Value = (int)population.Value,
                        Year = population.Year
                    });
                }
            }

            modelBuilder.Entity<Country>().HasData(countries);
            modelBuilder.Entity<CountryPopulation>().HasData(populations);
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
