using System;
using System.Threading.Tasks;
using Insurify.Population.Business.Domain;
using Insurify.Population.DataAccess.DbContext;
using Insurify.Population.DataAccess.Repository;
using Dapper;
using System.Linq;

namespace Insurify.Population.DataAccess.Implementation.Repository
{
    public class CountryPopulationRepository : BaseRepository<CountryPopulation, Guid>, ICountryPopulationRepository
    {
        public CountryPopulationRepository(IObjectContext context):base(context, "CountryPopulations")
        {
        }

        public Task<CountryPopulation> FindAsync(string country, int year)
        {
            return Task.Run(() => {
                return Context.Query<CountryPopulation>().FirstOrDefault(p => p.Country.Name == country && p.Year == year);
            });
        }

        public Task<IQueryable<CountryPopulation>> GetNearestPopulationsByYearAsync(string country, int year, int take)
        {
            return Task.Run(() => {
                return Context.Query<CountryPopulation>()
                    .Where(p => p.Country.Name == country)
                    .OrderBy(p => Math.Abs(year - p.Year))
                    .Take(take);
            });
        }
    }
}
