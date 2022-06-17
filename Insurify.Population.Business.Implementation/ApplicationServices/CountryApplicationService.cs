using System;
using Insurify.Population.Business.Contracts.ApplicationServices;
using Insurify.Population.Business.Domain;
using Insurify.Population.DataAccess.Repository;

namespace Insurify.Population.Business.Implementation.ApplicationServices
{
    public class CountryApplicationService : BaseApplicationService<Country, Guid>, ICountryApplicationService
    {
        public CountryApplicationService(ICountryRepository repository):base(repository)
        {
        }
    }
}
