using System.Threading.Tasks;
using Google.Api.Ads.AdWords.Lib;
using GoogleAPICore.Persistence.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GoogleAPICore.Controllers
{
    [Route("api/[controller]/[action]")]
    public class OAuthApiController : Controller
    {
        private readonly IOAuthRepository _repository;

        public OAuthApiController(IOAuthRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLogin(string baseUrl)
        {
            var response = _repository.ExternalLoginToGoogle(baseUrl);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Authorize(string authorizationCode)
        {
            AdWordsUser user = _repository.GetConfiguration();
           _repository.DoAuth2Authorization(user, authorizationCode);

            return Ok("Success");
        }   

        [HttpPost]
        public async Task<IActionResult> AuthenticateClient()
        {
            var result = _repository.AuthenticateUser();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> LoginInGoogle()
        {
            var result = _repository.LoginUsingGoogle();

            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> Configuration()
        {
            var result = _repository.GetConfiguration();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GoogleUserData(string access_token)
        {
            var result = _repository.GetGoogleUserData(access_token);
            return Ok(result);
        }

    }
}