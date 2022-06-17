using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Insurify.Population.DataAccess.Implementation.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Insurify.Population.DataAccess.DbContext;
using Insurify.Population.DataAccess.Repository;
using Insurify.Population.DataAccess.Implementation.Repository;
using Insurify.Population.Business.Contracts.ApplicationServices;
using Insurify.Population.Business.Implementation.ApplicationServices;
using Insurify.Population.Api.AppStart;

namespace Insurify.Population.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ObjectContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CountryPopulationDb")));
            services.AddControllers();

            services.AddTransient<IObjectContext, ObjectContext>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<ICountryPopulationRepository, CountryPopulationRepository>();

            services.AddTransient<ICountryApplicationService, CountryApplicationService>();
            services.AddTransient<ICountryPopulationApplicationService, CountryPopulationApplicationService>();

            AutoMapperConfig.CreateMappings();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
