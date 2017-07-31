using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebService.Classes;
using WebService.Models;
using WebService.Models.DatabaseModels;

namespace WebService.Controllers {
    public class FitBitController : Controller {
        private readonly IHelper _helper;

        public FitBitController(IHelper helper) {

            _helper = helper;
        }

        public ActionResult Authorize() {
            var requestUri = _helper.GenerateFitBitAuthenticationUri();
            return Redirect(requestUri);
        }

        public async Task<ActionResult> Callback() {
            try {
                var code = HttpContext.Request.Query["code"].ToString();

                if (code == "") {
                    ViewBag.Error = "Authorization was unsuccessful";
                    return View();
                }

                var authenticator = _helper.GetFitBitOAuthHelper();

                var accessToken = await authenticator.ExchangeAuthCodeForAccessTokenAsync(code);

                var tokenStorage = new TokenStorage {ProfileType = ProfileType.FitBit, FitBitToken = accessToken};

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