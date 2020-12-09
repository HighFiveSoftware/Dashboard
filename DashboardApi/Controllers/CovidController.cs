using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IEnumerable<CovidCase>> GetAllCasesFromCountry([FromQuery] string country)
        {
            if (country == null)
            {
                return await _covidService.GetCasesWorldWide(7);
            }

            return await _covidService.GetCasesByCountry(7, country.ToLower());
        }

        [HttpGet("topCountries")]
        public async Task<IEnumerable<CovidCase>> GetTopCountries([FromQuery] string sortBy)
        {
            var allowedKeys = new string[]
            {
                "confirmed_today", "deaths_today", "recovered_today", "confirmed_change", "deaths_change",
                "recovered_change"
            };
            if (sortBy == null || !allowedKeys.Contains(sortBy))
            {
                sortBy = "confirmed_today";
            }

            return await _covidService.GetTopCountries(20, sortBy, DateTime.Today.AddDays(-1));
        }

        [HttpGet("total")]
        public async Task<IEnumerable<CovidCaseTotal>> GetTotalCasesByCountry([FromQuery] string country)
        {
            if (country == null)
            {
                return await _covidService.GetTotalCasesWorldwide();
            }

            return await _covidService.GetTotalsCasesByCountry(country.ToLower());
        }
    }
}