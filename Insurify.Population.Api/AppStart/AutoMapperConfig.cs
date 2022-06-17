using System;
using AutoMapper;
using Insurify.Population.Api.Models;
using Insurify.Population.Business.Domain;

namespace Insurify.Population.Api.AppStart
{
    public class AutoMapperConfig
    {
        public static IMapper Mapper { get; private set; }

        public static void CreateMappings()
        {
            var mapperConfiguration = new MapperConfiguration((config) =>
            {
                config.CreateMap<Country, CountryViewModel>();
                config.CreateMap<CountryViewModel, Country>();

                config.CreateMap<CountryPopulation, CountryPopulationViewModel>();
                config.CreateMap<CountryPopulationViewModel, CountryPopulation>();
            });
            Mapper = mapperConfiguration.CreateMapper();
        }
    }
}
