using Google.Api.Ads.AdWords.Lib;
using GoogleAPICore.Controllers.Resources;
using System.Collections.Generic;

namespace GoogleAPICore.Persistence.Repository.Interface
{
    public interface IAdsRepository
    {
        IEnumerable<GetAdsResponse> GetAllApprovedAds(AdWordsUser user, long campaignId);
        GetCampaignResponse GetCampaign(AdWordsUser user, long customerId);
    }
}
