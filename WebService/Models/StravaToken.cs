using System;
using Newtonsoft.Json;

namespace WebService.Models {
    public class StravaToken {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("athlete")]
        public Athlete Athlete { get; set; }
    }

    public class Athlete {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("profile")]
        public string Profile { get; set; }

        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("sex")]
        public string Gender { get; set; }

        [JsonProperty("created_at")]
        public DateTime JoinDate { get; set; }

        [JsonProperty("premium")]
        public bool PremiumAccount { get; set; }
    }
}