using GoogleAPICore.Persistence.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GoogleAPICore.Controllers
{
    [Route("api/[controller]/[action]")]
    public class EncryptionApiController : Controller
    {
        private readonly IPasswordHashRepository _repository;

        public EncryptionApiController(IPasswordHashRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> EncryptPassword(string password)
        {
            var result = _repository.EncryptString(password);     
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> DecryptPassword(string password)
        {
            var result = _repository.DecryptString(password);
            return Ok(result);
        }
    }
}