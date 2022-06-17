using System;
using System.Linq;
using System.Threading.Tasks;
using Insurify.Population.Business.Domain;

namespace Insurify.Population.DataAccess.Repository
{
    public interface ICountryPopulationRepository : IRepository<CountryPopulation, Guid>
    {
        Task<CountryPopulation> FindAsync(string country, int year);
        Task<IQueryable<CountryPopulation>> GetNearestPopulationsByYearAsync(string country, int year, int take);
    }
}
