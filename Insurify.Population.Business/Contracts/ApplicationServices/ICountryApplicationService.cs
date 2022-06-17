using System;
using System.Threading.Tasks;
using Insurify.Population.Business.Domain;

namespace Insurify.Population.Business.Contracts.ApplicationServices
{
    public interface ICountryApplicationService : IApplicationService<Country, Guid>
    {
        Task<Country> FindAsync(string name);
    }
}
