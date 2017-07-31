using System.Text;
using Fitbit.Api.Portable;
using Fitbit.Api.Portable.OAuth2;
using Microsoft.Extensions.Options;
using WebService.Models;
using WebService.Models.DatabaseModels;

namespace WebService.Classes {
    public interface IHelper {
        OAuth2Helper GetFitBitOAuthHelper();
        FitbitAppCredentials GetFitBitCreds();
        string GenerateStravaAuthenticationUri();
        string GenerateFitBitAuthenticationUri();
        string[] GetStravaCreds();
    }

    public class Helper : IHelper {
        private readonly AppSettings _appSettings;

        public Helper(IOptions<AppSettings> appSettings) {
            _appSettings = appSettings.Value;
        }

        public OAuth2Helper GetFitBitOAuthHelper() {
            return new OAuth2Helper(GetFitBitCreds(), _appSettings.FitBitCredentials.Callback);
        }

        public FitbitAppCredentials GetFitBitCreds() {
            return new FitbitAppCredentials {ClientId = _appSettings.FitBitCredentials.ClientId, ClientSecret = _appSettings.FitBitCredentials.ClientSecret};
        }

        public string[] GetStravaCreds() {
            return new[] {_appSettings.StravaCredentials.ClientId, _appSettings.StravaCredentials.ClientSecret};
        }

        public string GenerateStravaAuthenticationUri() {
            var builder = new StringBuilder();

            builder.Append("https://www.strava.com/oauth/authorize?client_id=");
            builder.Append(_appSettings.StravaCredentials.ClientId);
            builder.Append("&response_type=code");
            builder.Append("&redirect_uri=");
            builder.Append(_appSettings.StravaCredentials.Callback);
            builder.Append("&scope=view_private,write");
            builder.Append("&approval_prompt=force");

            return builder.ToString();
        }

        public string GenerateFitBitAuthenticationUri() {
            var builder = new StringBuilder();

            builder.Append("https://www.fitbit.com/oauth2/authorize?response_type=code&client_id=");
            builder.Append(_appSettings.FitBitCredentials.ClientId);
            builder.Append("&response_type=code");
            builder.Append("&scope=profile weight");
            builder.Append("&redirect_uri=");
            builder.Append(_appSettings.FitBitCredentials.Callback);

            return builder.ToString();
        }

    }
}