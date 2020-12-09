using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DashboardApi.Models;

namespace DashboardApi.Services
{
    public interface ICovidService
    {
        Task<IEnumerable<CovidCase>> GetCasesByCountry(int limit, string countryName);
        Task<IEnumerable<CovidCase>> GetCasesWorldWide(int limit);
        Task<IEnumerable<CovidCase>> GetTopCountries(int limit, string sortBy, DateTime day);
        Task<IEnumerable<CovidCaseTotal>> GetTotalsCasesByCountry(string countryName);
        Task<IEnumerable<CovidCaseTotal>> GetTotalCasesWorldwide();
    }

    public class CovidService : ICovidService
    {
        public IDbConnection _connection { get; set; }

        public CovidService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<CovidCase>> GetCasesByCountry(int limit, string countryName)
        {
            var cases = await _connection.QueryAsync<CovidCase>(
                "SELECT country_region, entry_date, confirmed_today, deaths_today, recovered_today, confirmed_change, deaths_change, recovered_change FROM covid19_cases_jk_aggregate_view WHERE lower(country_region)=@countryName ORDER BY entry_date DESC LIMIT @limit",
                new {countryName, limit});

            return cases;
        }

        public async Task<IEnumerable<CovidCase>> GetCasesWorldWide(int limit)
        {
            var cases = await _connection.QueryAsync<CovidCase>(
                "SELECT 'worldwide' as country_region,entry_date, confirmed_today, deaths_today, recovered_today, confirmed_change, deaths_change, recovered_change FROM covid19_cases_jk_aggregate_worldwide_view ORDER BY entry_date DESC LIMIT @limit",
                new {limit});

            return cases;
        }

        public async Task<IEnumerable<CovidCase>> GetTopCountries(int limit, string sortBy, DateTime day)
        {
            var topCountries = await _connection.QueryAsync<CovidCase>(
                $"SELECT country_region, entry_date, confirmed_today, deaths_today, recovered_today, confirmed_change, deaths_change, recovered_today FROM covid19_cases_jk_aggregate_view WHERE entry_date = date_trunc('day', @day) ORDER BY {sortBy} DESC LIMIT @limit",
                new { sortBy, limit, day });

            return topCountries;
        }

        public async Task<IEnumerable<CovidCaseTotal>> GetTotalsCasesByCountry(string countryName)
        {
            var totalCases = await _connection.QueryAsync<CovidCaseTotal>(
               "SELECT confirmed_today as confirmed_total, deaths_today as deaths_total, recovered_today as recovered_total FROM covid19_cases_jk_aggregate_view WHERE lower(country_region) = @countryName ORDER BY entry_date DESC LIMIT 1",
                new { countryName});

            return totalCases;
        }

        public async Task<IEnumerable<CovidCaseTotal>> GetTotalCasesWorldwide()
        {
            var totalCases = await _connection.QueryAsync<CovidCaseTotal>(
                "SELECT  confirmed_today as confirmed_total, deaths_today as deaths_total, recovered_today as recovered_total FROM covid19_cases_jk_aggregate_worldwide_view ORDER BY entry_date DESC LIMIT 1");

            return totalCases;
        }
    }
}