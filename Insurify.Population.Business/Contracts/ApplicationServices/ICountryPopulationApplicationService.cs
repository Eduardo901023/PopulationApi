using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Insurify.Population.Business.Domain;

namespace Insurify.Population.Business.Contracts.ApplicationServices
{
    public interface ICountryPopulationApplicationService : IApplicationService<CountryPopulation, Guid>
    {
        Task<CountryPopulation> PredictPopulationAsync(string country, int year);
        Task<IEnumerable<CountryPopulation>> PredictPopulationAsync(int year, string sort = "desc", int top = 20);
    }
}
