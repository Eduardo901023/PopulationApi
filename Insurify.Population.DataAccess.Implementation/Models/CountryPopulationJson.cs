using System;
using System.Text.Json.Serialization;

namespace Insurify.Population.DataAccess.Implementation.Models
{
    public class CountryPopulationJson
    {
        [JsonPropertyName("Country Code")]
        public string CountryCode { get; set; }
        [JsonPropertyName("Country Name")]
        public string CountryName { get; set; }
        public float Value { get; set; }
        public int Year { get; set; }
    }
}
