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
        Task<IEnumerable<CovidCaseCountry>> GetCasesByCountry(int limit, string countryName);
        Task<IEnumerable<CovidCaseWorldwide>> GetCasesWorldWide(int limit);
    }

    public class CovidService : ICovidService
    {
        public IDbConnection _connection { get; set; }

        public CovidService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<CovidCaseCountry>> GetCasesByCountry(int limit, string countryName)
        {
            var cases = await _connection.QueryAsync<CovidCaseCountry>(
                "SELECT country_region, entry_date, confirmed_today, deaths_today, recovered_today, confirmed_change, deaths_change, recovered_change FROM covid19_cases_jk_aggregate_view WHERE lower(country_region)=@countryName ORDER BY entry_date DESC LIMIT @limit",
                new {countryName, limit});

            return cases;
        }

        public Task<IEnumerable<CovidCaseWorldwide>> GetCasesWorldWide(int limit)
        {
            throw new NotImplementedException();
        }
    }
}