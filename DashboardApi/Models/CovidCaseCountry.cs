using System;
using Newtonsoft.Json;

namespace DashboardApi.Models
{
    public class CovidCaseCountry
    {
        [JsonProperty("country_region")]
        public string CountryRegion { get; set; }
        
        [JsonProperty("entry_date")]
        public DateTime EntryDate { get; set; }
        
        [JsonProperty("confirmed_today")]
        public int ConfirmedToday { get; set; }
        
        [JsonProperty("deaths_today")]
        public int DeathsToday { get; set; }
        
        [JsonProperty("recovered_today")]
        public int RecoveredToday { get; set; }
        
        [JsonProperty("confirmed_change")]
        public int ConfirmedChange { get; set; }
        
        [JsonProperty("deaths_change")]
        public int DeathsChange { get; set; }
        
        [JsonProperty("recovered_change")]
        public int RecoveredChange { get; set; }
    }
}