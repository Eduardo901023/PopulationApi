using System;
using Insurify.Population.Business.Domain;

namespace Insurify.Population.DataAccess.Repository
{
    public interface ICountryRepository : IRepository<Country, Guid>
    {
    }
}
