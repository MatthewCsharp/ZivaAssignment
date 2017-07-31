using Fitbit.Api.Portable.OAuth2;
using WebService.Models.DatabaseModels;

namespace WebService.Models {
    public class TokenStorage {
        public ProfileType ProfileType { get; set; }
        public OAuth2AccessToken FitBitToken { get; set; }
        public StravaToken StravaToken { get; set; }
    }
}