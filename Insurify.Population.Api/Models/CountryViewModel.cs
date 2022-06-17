using System;
namespace Insurify.Population.Api.Models
{
    public class CountryViewModel : EntityViewModel<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
