using System;
using System.Threading.Tasks;
using Insurify.Population.Business.Domain;
using Insurify.Population.DataAccess.DbContext;
using Insurify.Population.DataAccess.Repository;
using Dapper;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Insurify.Population.DataAccess.Implementation.Repository
{
    public class CountryPopulationRepository : BaseRepository<CountryPopulation, Guid>, ICountryPopulationRepository
    {
        public CountryPopulationRepository(IObjectContext context):base(context, "CountryPopulations")
        {
        }

        public Task<CountryPopulation> FindAsync(string country, int year)
        {
            return Task.Run(() =>
            {
                return Context.Query<CountryPopulation>()
                .Include(p => p.Country)
                .FirstOrDefault(p => p.Country.Name == country && p.Year == year);
            });
        }

        public Task<IQueryable<CountryPopulation>> GetAllByYearAsync(int year, int skip, int take)
        {
            return Task.Run(() =>
            {
                return Context.Query<CountryPopulation>()
                .Include(p => p.Country)
                .Where(p => p.Year == year)
                .Skip(skip)
                .Take(take);
            });
        }

        public Task<IQueryable<CountryPopulation>> GetNearestPopulationsByYearAsync(string country, int year, int take)
        {
            return Task.Run(() => {
                return Context.Query<CountryPopulation>()
                    .Include(p => p.Country)
                    .Where(p => p.Country.Name == country)
                    .OrderBy(p => Math.Abs(year - p.Year))
                    .Take(take);
            });
        }

        public async Task<IEnumerable<CountryPopulation>> GetNearestPopulationsByYearAsync(int year, int take)
        {
            var query = $@"WITH p AS (SELECT c.Name,c.Code,cp.*, ABS(cp.Year - {year}) AS x FROM {TableName} AS cp
                            INNER JOIN Countries AS c ON c.Id = cp.CountryId
                            )

                            select * from (SELECT *, row_number() over (partition by p.Name order by p.x) as z
                            FROM p) as c
                            WHERE c.z <= {take}
                            ORDER BY c.Name";

            using (var sqlConnection = GetConnection())
            {
                var result = await sqlConnection.QueryAsync<string, string, CountryPopulation, int, long, CountryPopulation>(query,
                    (cn, cc, cp, x, z) =>
                    {
                        var country = new Country()
                        {
                            Id = cp.CountryId,
                            Code = cc,
                            Name = cn
                        };

                        cp.Country = country;

                        return cp;
                    },
                splitOn: "Code,Id,x,z").ConfigureAwait(false);
                return result;
            }
        }
    }
}
