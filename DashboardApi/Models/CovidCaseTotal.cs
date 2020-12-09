using System;
using Newtonsoft.Json;

namespace DashboardApi.Models
{
    public class CovidCaseTotal
    {

        [JsonProperty("confirmed_total")]
        public int ConfirmedTotal { get; set; }

        [JsonProperty("deaths_total")]
        public int DeathsTotal { get; set; }

        [JsonProperty("recovered_total")]
        public int RecoveredTotal { get; set; }
    }
}