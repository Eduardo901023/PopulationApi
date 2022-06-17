using System;
using Insurify.Population.Business.Domain;

namespace Insurify.Population.Business.Contracts.ApplicationServices
{
    public interface ICountryApplicationService : IApplicationService<Country, Guid>
    {
    }
}
