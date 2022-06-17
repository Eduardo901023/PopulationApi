using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Insurify.Population.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PopulationController : ControllerBase
    {

        private readonly ILogger<PopulationController> _logger;

        public PopulationController(ILogger<PopulationController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{country}/year/{year}")]
        public async Task<IActionResult> Get(string country, int year)
        {
          
        }
    }
}
