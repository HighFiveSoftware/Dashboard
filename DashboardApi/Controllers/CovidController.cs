using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DashboardApi.Models;
using DashboardApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DashboardApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("cases")]
    public class CovidController : ControllerBase
    {
        private ICovidService _covidService { get; }

        public CovidController(ICovidService covidService)
        {
            _covidService = covidService;
        }

        // [Route("allCases")]
        [HttpGet("")]
        public async Task<IActionResult> GetAllCasesFromCountry([FromQuery] string country)
        {
            if (country == null)
            {
                return Ok( new {country="worldwide", cases = await _covidService.GetCasesWorldWide(7)});
            }
            return Ok(new {country, cases = await _covidService.GetCasesByCountry(7, country.ToLower())});
        }
    }
}