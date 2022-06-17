using System;
using System.Threading.Tasks;
using Insurify.Population.Business.Domain;

namespace Insurify.Population.DataAccess.Repository
{
    public interface ICountryRepository : IRepository<Country, Guid>
    {
        Task<Country> FindAsync(string name);
    }
}
