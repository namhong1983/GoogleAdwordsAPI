using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201802;
using GoogleAPICore.Controllers.Resources;
using GoogleAPICore.Models;
using GoogleAPICore.Persistence.Repository.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace GoogleAPICore.Persistence.Repository
{
    public class AdsRepository:IAdsRepository
    {
        private readonly AdWordsConfig _config;

        public AdsRepository(IOptions<AdWordsConfig> config)
        {
            _config = config.Value;
        }

        public GetCampaignResponse GetCampaign(AdWordsUser user, long customerId)
        {
            var result = new GetCampaignResponse();
            using (CampaignService campaignService = (CampaignService)user.GetService(AdWordsService.v201802.CampaignService))
            {
                // Create the selector.
                Selector selector = new Selector()
                {
                    fields = new string[] 
                    {
                      Campaign.Fields.Id,
                      Campaign.Fields.Name,
                      Campaign.Fields.Status
                     },
                    paging = Paging.Default
                };
                
                Predicate predicate = new Predicate();
                predicate.field = "CampaignId";
                predicate.@operator = PredicateOperator.EQUALS;
                predicate.values = new String[] { customerId.ToString() };
                selector.predicates = new Predicate[] { predicate };

                CampaignPage page = new CampaignPage();

                try
                {
                    page = campaignService.get(selector);

                    if (page != null && page.entries != null)
                    {
                        foreach (Campaign campaign in page.entries)
                        {
                            result.CampaignId = campaign.id;
                            result.CampaignName = campaign.name;                       
                        }
                    }
                    else
                    {
                        result.CampaignId = 1;
                        result.CampaignName = "Test Campaign";
                    }
                }
                catch (Exception x)
                {
                    throw new Exception(x.Message);
                }
                return result;
            }
        }

        public IEnumerable<GetAdsResponse> GetAllApprovedAds(AdWordsUser user, long campaignId)
         {
            var result = new List<GetAdsResponse>();
            using (AdGroupAdService adGroupAdService = (AdGroupAdService)user.GetService(AdWordsService.v201802.AdGroupAdService))
            {
                try
                {
                    // Create the selector.
                    Selector selector = new Selector()
                    {
                        fields = new string[] 
                        {
                            Ad.Fields.Id,
                            AdGroupAd.Fields.PolicySummary
                        },
                        predicates = new Predicate[] 
                        {
                            Predicate.Equals(AdGroup.Fields.CampaignId, campaignId),
                            Predicate.Equals(AdGroupAdPolicySummary.Fields.CombinedApprovalStatus, PolicyApprovalStatus.APPROVED.ToString())
                        },
                        paging = Paging.Default
                    };

                    AdGroupAdPage page = new AdGroupAdPage();
                    int approvedAdsCount = 0;

                    page = adGroupAdService.get(selector);

                    if (page.entries != null)
                    {
                        foreach (AdGroupAd adGroupAd in page.entries)
                        {
                            AdGroupAdPolicySummary policySummary = adGroupAd.policySummary;
                            approvedAdsCount++;
                            result.Add(new GetAdsResponse
                            {
                                AdId = adGroupAd.ad.id,
                                AdHeadings = adGroupAd.ad.displayUrl
                            });
                        }
                    }
                    else
                    {
                        //Test Account
                        result.Add(new GetAdsResponse
                        {
                            AdId = 2,
                            AdHeadings = "Heading Sample 2"
                        });
                        result.Add(new GetAdsResponse
                        {
                            AdId = 3,
                            AdHeadings = "Heading Sample 3"
                        });
                    }
                }
                catch (Exception x)
                {
                    throw new Exception(x.Message);
                }
                return result;
            }
        }
    }
}
