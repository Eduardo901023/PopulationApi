using System;
using Insurify.Population.Business.Domain;
using Insurify.Population.DataAccess.DbContext;
using Insurify.Population.DataAccess.Repository;

namespace Insurify.Population.DataAccess.Implementation.Repository
{
    public class CountryRepository : BaseRepository<Country, Guid>, ICountryRepository
    {
        public CountryRepository(IObjectContext context):base(context, "Countries")
        {
        }
    }
}
