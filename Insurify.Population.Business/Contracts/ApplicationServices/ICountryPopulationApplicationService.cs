using System;
using System.Threading.Tasks;
using Insurify.Population.Business.Domain;

namespace Insurify.Population.Business.Contracts.ApplicationServices
{
    public interface ICountryPopulationApplicationService : IApplicationService<CountryPopulation, Guid>
    {
        Task<CountryPopulation> PredictPopulationAsync(string country, int year);
    }
}
