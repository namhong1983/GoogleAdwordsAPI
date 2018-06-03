using Google.Api.Ads.AdWords.Lib;
using GoogleAPICore.Persistence.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GoogleAPICore.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AdAccountsApiController : Controller
    {
        private readonly IAdAccountsRepository _repository;
        private readonly IOAuthRepository _oauthRepository;

        public AdAccountsApiController(IAdAccountsRepository repository, IOAuthRepository oauthRepository)
        {
            _repository = repository;
            _oauthRepository = oauthRepository;
        }

        [HttpGet]
        public IActionResult GetAllAdAccount()
        {
            #region Get AccountTree
            //AdWordsUser user = new AdWordsUser();
            //var result = _repository.GetAdAccountsTree(user);
            #endregion

            #region GetAllAccounts
            AdWordsUser user = _oauthRepository.GetConfiguration();
            var result = _repository.GetAllAccounts(user);
            #endregion

            return Ok(result);
        }

    }
}