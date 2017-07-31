namespace WebService.Models {
    public class AppSettings {
        public FitBitCredentials FitBitCredentials { get; set; }
        public StravaCredentials StravaCredentials { get; set; }
    }

    public class FitBitCredentials {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Callback { get; set; }
    }

    public class StravaCredentials {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Callback { get; set; }
    }
}