using System;
using Newtonsoft.Json;

namespace DashboardApi.Models
{
    public class CovidCaseTotal
    {

        [JsonProperty("confirmed_total")]
        public Int64 ConfirmedTotal { get; set; }

        [JsonProperty("deaths_total")]
        public Int64 DeathsTotal { get; set; }

        [JsonProperty("recovered_total")]
        public Int64 RecoveredTotal { get; set; }
    }
}