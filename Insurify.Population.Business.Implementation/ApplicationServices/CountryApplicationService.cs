using System;
using System.Threading.Tasks;
using Insurify.Population.Business.Contracts.ApplicationServices;
using Insurify.Population.Business.Domain;
using Insurify.Population.DataAccess.Repository;

namespace Insurify.Population.Business.Implementation.ApplicationServices
{
    public class CountryApplicationService : BaseApplicationService<Country, Guid>, ICountryApplicationService
    {
        private readonly ICountryRepository _repository;
        public CountryApplicationService(ICountryRepository repository):base(repository)
        {
            _repository = repository;
        }

        public Task<Country> FindAsync(string name)
        {
            return _repository.FindAsync(name);
        }
    }
}
