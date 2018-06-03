using GoogleAPICore.Models;
using GoogleAPICore.Persistence.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GoogleAPICore.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CrendentialsApiController : Controller
    {
        private readonly ICredentialsRepository _repository;

        public CrendentialsApiController(ICredentialsRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> SaveCredentials([FromBody] OAuthLogin oauthLogin)
        {
            _repository.SaveLoginCredentials(oauthLogin);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetCredential(string email)
        {
            var result = new OAuthLogin();

            try
            {
                result = _repository.GetCredentialByEmail(email);
            }
            catch (Exception x)
            { 
                return BadRequest(x.Message);
            }
            return Ok(result);
        }
    }
}