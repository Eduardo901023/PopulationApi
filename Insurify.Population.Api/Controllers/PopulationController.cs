using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Insurify.Population.Api.Models;
using Insurify.Population.Business.Contracts.ApplicationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Insurify.Population.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PopulationController : BaseController
    {

        private readonly ILogger<PopulationController> _logger;
        private readonly ICountryPopulationApplicationService _countryPopulationApplicationService;
        private readonly ICountryApplicationService _countryApplicationService;

        public PopulationController(ILogger<PopulationController> logger,
            ICountryPopulationApplicationService countryPopulationApplicationService,
            ICountryApplicationService countryApplicationService)
        {
            _logger = logger;
            _countryPopulationApplicationService = countryPopulationApplicationService;
            _countryApplicationService = countryApplicationService;
        }

        [HttpGet("{countryName}/year/{year}")]
        public async Task<IActionResult> Predict(string countryName, int year)
        {
            var country = await _countryApplicationService.FindAsync(countryName);
            if (country == null)
                return NotFound("Country not found.");


            var result = await _countryPopulationApplicationService.PredictPopulationAsync(countryName, year);
            if (result == null)
                return NotFound($"Unable to predict {country}'s population in {year}.");

            var model = Mapper.Map<CountryPopulationViewModel>(result);
            return Ok(model);
        }

        [HttpGet("{year}")]
        public async Task<IActionResult> Predict(int year, [FromQuery] int top = 20, [FromQuery] string sort = "desc")
        {
            if (top < 1)
                return BadRequest("'top' parameter must be greater than 0");

            if (sort != "desc" && sort != "asc")
                return BadRequest("Possible values for parameter 'sort' are 'desc' or 'asc'");

            var result = await _countryPopulationApplicationService.PredictPopulationAsync(year, sort, top);

            var model = Mapper.Map<IEnumerable<CountryPopulationViewModel>>(result);
            return Ok(model);
        }
    }
}
