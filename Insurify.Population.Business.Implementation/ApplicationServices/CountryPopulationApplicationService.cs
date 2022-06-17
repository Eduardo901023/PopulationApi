using System;
using System.Linq;
using System.Threading.Tasks;
using Insurify.Population.Business.Contracts.ApplicationServices;
using Insurify.Population.Business.Domain;
using Insurify.Population.DataAccess.Repository;

namespace Insurify.Population.Business.Implementation.ApplicationServices
{
    public class CountryPopulationApplicationService : BaseApplicationService<CountryPopulation, Guid>, ICountryPopulationApplicationService
    {
        private new readonly ICountryPopulationRepository Repository;

        public CountryPopulationApplicationService(ICountryPopulationRepository repository):base(repository)
        {
            Repository = repository;
        }

        public async Task<CountryPopulation> PredictPopulationAsync(string country, int year)
        {
            var population = await Repository.FindAsync(country, year);
            if (population != null)
                return population;

            var nearestPopulation = (await Repository.GetNearestPopulationsByYearAsync(country, year, 2)).ToArray();
            if (nearestPopulation == null || nearestPopulation.Length < 2)
                return null;

            var firstP = nearestPopulation.First(p => p.Year == nearestPopulation.Min(a => a.Year));
            var secondP = nearestPopulation.First(p => p.Year == nearestPopulation.Max(a => a.Year));

            var m = (secondP.Value - firstP.Value) / (secondP.Year - firstP.Year);
            var prediction = m * year + (firstP.Value - m * firstP.Year);

            return new CountryPopulation()
            {
                Year = year,
                Value = prediction
            };
        }
    }
}
