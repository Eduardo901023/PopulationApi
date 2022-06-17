using System;
namespace Insurify.Population.Business.Domain
{
    public class CountryPopulation : Entity
    {
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
        public long Value { get; set; }
        public int Year { get; set; }
    }
}
