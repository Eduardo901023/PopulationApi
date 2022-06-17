using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Insurify.Population.Business.Contracts.ApplicationServices;
using Insurify.Population.Business.Domain;
using Insurify.Population.DataAccess.Repository;

namespace Insurify.Population.Business.Implementation.ApplicationServices
{
    public class CountryPopulationApplicationService : BaseApplicationService<CountryPopulation, Guid>, ICountryPopulationApplicationService
    {
        private  readonly ICountryPopulationRepository _repository;

        public CountryPopulationApplicationService(ICountryPopulationRepository repository):base(repository)
        {
            _repository = repository;
        }

        public async Task<CountryPopulation> PredictPopulationAsync(string country, int year)
        {
            var population = await _repository.FindAsync(country, year);
            if (population != null)
                return population;

            var nearestPopulation = (await _repository.GetNearestPopulationsByYearAsync(country, year, 2).ConfigureAwait(false)).ToArray();
            if (nearestPopulation == null || nearestPopulation.Length == 0)
                return null;

            if (nearestPopulation.Length == 1)
                return nearestPopulation[0];

            var firstP = nearestPopulation.First(p => p.Year == nearestPopulation.Min(a => a.Year));
            var secondP = nearestPopulation.First(p => p.Year == nearestPopulation.Max(a => a.Year));

            var m = (secondP.Value - firstP.Value) / (secondP.Year - firstP.Year);
            var prediction = m * year + (firstP.Value - m * firstP.Year);

            nearestPopulation[0].Value = Math.Max(prediction, 0);

            return nearestPopulation[0];
        }

        public async Task<IEnumerable<CountryPopulation>> PredictPopulationAsync(int year, string sort = "desc", int top = 20)
        {
            var populations = await _repository.GetAllByYearAsync(year, 0, top);
            if (populations != null && populations.Count() > 0)
            {
                populations = sort == "desc" ? populations.OrderByDescending(p => p.Value) : populations.OrderBy(p => p.Value);
                return populations.Take(top);
            }

            var predictions = new List<CountryPopulation>();
            var nearestPopulations = (await _repository.GetNearestPopulationsByYearAsync(year, 2).ConfigureAwait(false)).GroupBy(p => p.CountryId);
            foreach (var countryPopulation in nearestPopulations)
            {
                var countryPopulationArray = countryPopulation.ToArray();
                if (countryPopulationArray.Length == 1)
                    predictions.Add(countryPopulationArray[0]);
                else
                {
                    var firstP = countryPopulationArray.First(p => p.Year == countryPopulationArray.Min(a => a.Year));
                    var secondP = countryPopulationArray.First(p => p.Year == countryPopulationArray.Max(a => a.Year));

                    var m = (secondP.Value - firstP.Value) / (secondP.Year - firstP.Year);
                    var prediction = m * year + (firstP.Value - m * firstP.Year);

                    countryPopulationArray[0].Value = Math.Max(prediction, 0);
                    predictions.Add(countryPopulationArray[0]);
                }
            }

            predictions = sort == "desc" ? predictions.OrderByDescending(p => p.Value).ToList() : predictions.OrderBy(p => p.Value).ToList();
            return predictions.Take(top);
        }
    }
}
