using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebService.Classes;
using WebService.Models;
using WebService.Models.DatabaseModels;

namespace WebService.Controllers {
    public class StravaController : Controller {
        private readonly IHelper _helper;
        private readonly RequestHelper _requestHelper;

        public StravaController(RequestHelper requestHelper, IHelper helper) {
            _requestHelper = requestHelper;
            _helper = helper;
        }

        public ActionResult Authorize() {
            var requestUri = _helper.GenerateStravaAuthenticationUri();
            return Redirect(requestUri);
        }

        public async Task<ActionResult> CallBack() {
            try {
                var code = HttpContext.Request.Query["code"].ToString();

                if (code == "") {
                    ViewBag.Error = "Authorization was unsuccessful";
                    return View();
                }

                var creds = _helper.GetStravaCreds();
                var uri = $"https://www.strava.com/oauth/token?client_id={creds[0]}&client_secret={creds[1]}&code={code}";
                var tokenExchange = await _requestHelper.Client.PostAsync(uri, null).Result.Content.ReadAsStringAsync();

                var stravaResult = JsonConvert.DeserializeObject<StravaToken>(tokenExchange);

                var tokenStorage = new TokenStorage {ProfileType = ProfileType.Strava, StravaToken = stravaResult};

                var serializedToken = JsonConvert.SerializeObject(tokenStorage);
                HttpContext.Session.SetString("Token", serializedToken);

                return RedirectToAction("Profile", "Home");
            }
            catch (Exception e) {
                ViewBag.Error = e.Message;
                return View();
            }
        }
    }
}