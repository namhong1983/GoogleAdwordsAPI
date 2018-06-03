using Google.Api.Ads.AdWords.Lib;
using GoogleAPICore.Controllers.Resources;
using GoogleAPICore.Persistence.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GoogleAPICore.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AdsApiController : Controller
    {
        private readonly IAdsRepository _repository;
        private readonly IOAuthRepository _oauthRepository;
        public AdsApiController(IAdsRepository repository, IOAuthRepository oauthRepository)
        {
            _repository = repository;
            _oauthRepository = oauthRepository;
        }

        [HttpGet]
        public IActionResult GetAllApprovedAds(GetAdsRequest ads)
        {
            AdWordsUser user = _oauthRepository.GetConfiguration();
            var campaign = _repository.GetCampaign(user, ads.CustomerId);
            var result = _repository.GetAllApprovedAds(user,campaign.CampaignId);

            return Ok(result);
        }
    }
}