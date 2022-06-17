using System;
namespace Insurify.Population.Api.Models
{
    public class CountryPopulationViewModel : EntityViewModel<Guid>
    {
        public Guid CountryId { get; set; }
        public CountryViewModel Country { get; set; }
        public int Value { get; set; }
        public int Year { get; set; }
    }
}
