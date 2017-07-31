using System;
using System.Threading.Tasks;
using Fitbit.Api.Portable;
using Fitbit.Api.Portable.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebService.Classes;
using WebService.Models;
using WebService.Models.DatabaseModels;

namespace WebService.Controllers {
    public class HomeController : Controller {
        private readonly ZivaContext _context;
        private readonly IHelper _helper;

        public HomeController(IHelper helper, ZivaContext context) {
            _helper = helper;
            _context = context;
        }

        public IActionResult Index() {
            return View();
        }

        public async Task<IActionResult> Users() {
            return View(await _context.UserStats.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> Profile() {

            var tokenStorage = JsonConvert.DeserializeObject<TokenStorage>(HttpContext.Session.GetString("Token"));

            UserStats model;

            if (tokenStorage.ProfileType == ProfileType.FitBit) {
                var client = GetFitbitClient(tokenStorage.FitBitToken);
                var profile = await client.GetUserProfileAsync();

                model = new UserStats {
                    UserId = profile.EncodedId,
                    Name = profile.FullName,
                    Height = profile.Height,
                    Weight = profile.Weight,
                    DateOfBirth = profile.DateOfBirth,
                    Gender = profile.Gender.ToString(),
                    Avatar = profile.Avatar,
                    ProfileType = ProfileType.FitBit,
                    UnitType = profile.WeightUnit == "METRIC" ? UnitType.Metric : UnitType.Imperial
                };
            }
            else {
                var stravaToken = tokenStorage.StravaToken;
                model = new UserStats {
                    UserId = stravaToken.Athlete.Id.ToString(),
                    Name = $"{stravaToken.Athlete.FirstName} {stravaToken.Athlete.LastName}",
                    JoinDate = stravaToken.Athlete.JoinDate,
                    Gender = stravaToken.Athlete.Gender == "M" ? "MALE" : "FEMALE",
                    Avatar = stravaToken.Athlete.Profile,
                    Premium = stravaToken.Athlete.PremiumAccount,
                    ProfileType = ProfileType.Strava
                };
            }


            return View(model);
        }

        private FitbitClient GetFitbitClient(OAuth2AccessToken token) {
            if (token != null) {
                return new FitbitClient(_helper.GetFitBitCreds(), token);
            }
            throw new Exception("First time requesting a FitbitClient from the session you must pass the AccessToken.");
        }
    }
}