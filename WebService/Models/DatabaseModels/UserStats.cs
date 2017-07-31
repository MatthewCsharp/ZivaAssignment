using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebService.Models.DatabaseModels {
    public class UserStats {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public bool Premium { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? JoinDate { get; set; }
        public string Gender { get; set; }
        public string Avatar { get; set; }
        public ProfileType ProfileType { get; set; }
        public UnitType UnitType { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProfileType {
        FitBit = 1,
        Strava = 2
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum UnitType {
        Metric = 1,
        Imperial = 2
    }
}