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
        public async Task<IActionResult> GetAllCasesFromCountry([FromQuery] string country)
        {
            if (country == null)
            {
                return Ok(new {country = "worldwide", cases = await _covidService.GetCasesWorldWide(7)});
            }

            return Ok(new {country, cases = await _covidService.GetCasesByCountry(7, country.ToLower())});
        }

        [HttpGet("topCountries")]
        public async Task<IActionResult> GetTopCountries([FromQuery] string sortBy)
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

            return Ok(new { sorted_by = sortBy, topCountries = await _covidService.GetTopCountries(20, sortBy, DateTime.Today.AddDays(-1)) });
        }

        [HttpGet("countCountry")]
        public async Task<IActionResult> GetAllCasesCountFromCountry([FromQuery] string country)
        {
            if (country == null)
            {
                return Ok(new { country = "worldwide", cases = await _covidService.GetCasesCountWorldWide() });
            }

            return Ok(new { country, cases = await _covidService.GetCasesCountTotalByCountry(country.ToLower()) });
        }

    }
}