using GoogleAPICore.API;
using GoogleAPICore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GoogleAPICore.Controllers
{
    public class LoginController : Controller
    {
        private readonly RestClientBase _clientBaseAddress;

        public LoginController(IOptions<RestClientBase> clientBaseAddress)
        {
            _clientBaseAddress = clientBaseAddress.Value;
        }

        public IActionResult LoginView()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginAccount(Login account)
        {
            var record = LoginAPI.CheckUserExists(account.Email, _clientBaseAddress.BaseURLApi);

            if (account == null || record == null)
            {
                return RedirectToAction("LoginView", "Login");
            }
            else
            {
                var decryptedPassword = EncryptionAPI.DecryptPassword(record.Password,_clientBaseAddress.BaseURLApi);
                if (account.Password == decryptedPassword)
                {
                    return RedirectToAction("Index", "AdAccounts");
                }
                else
                {
                    return BadRequest("Invalid Credentials");
                }
            }

            //var googleUserData = OAuthAPI.GetGoogleUserData(account.AccessToken, _clientBaseAddress.BaseURLApi);
            //if (account.Email == googleUserData.email)
            //{
            //    return RedirectToAction("Index", "AdAccounts");
            //}
            //else
            //{ 
            //    return BadRequest("Invalid Credentials");
            //}
        }

        public IActionResult LoginWithGoogle()
        {
            var page = OAuthAPI.GetGoogleLogin(_clientBaseAddress.BaseURLApi);
            return Redirect(page);
        }

        public IActionResult LoginGoogleAccount()
        {
            return PartialView(); 
        }

        public IActionResult LoginGooglePassword(string email)
        {
            var oauthLogin = new OAuthLogin();
            oauthLogin.Email = email;
            oauthLogin.Password = string.Empty;

            return PartialView(oauthLogin);
        }

        public IActionResult AuthorizationToAccess(string email, string password)
        {
            var oauthLogin = new OAuthLogin();
            oauthLogin.Email = email;
            oauthLogin.Password = password;

            return PartialView(oauthLogin);
        }

        public IActionResult SaveGoogleAccount(OAuthLogin oauthLogin)
        {
            var record = LoginAPI.CheckUserExists(oauthLogin.Email, _clientBaseAddress.BaseURLApi);

            if (record == null)
            {
                var response = OAuthAPI.GenerateToken(_clientBaseAddress.BaseURLApi);
                oauthLogin.Token = response.access_token;
                LoginAPI.SaveCredentials(oauthLogin, _clientBaseAddress.BaseURLApi);
            }

            return RedirectToAction("Index", "AdAccounts");
        }


    }
}