using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DashboardApi.Models
{
    public class RefreshTokenDto
    {
        [Required]
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}