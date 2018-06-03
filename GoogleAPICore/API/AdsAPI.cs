using GoogleAPICore.Controllers.Resources;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace GoogleAPICore.API
{
    public static class AdsAPI
    {
        public static RestClient _client;

        public static IEnumerable<GetAdsResponse> GetAds(GetAdsRequest ads, string baseUrl)
        {
            _client = new RestClient(baseUrl);
            var apiRequest = new RestRequest(Method.GET);
            apiRequest.Resource = "/AdsApi/GetAllApprovedAds";
            apiRequest.RequestFormat = DataFormat.Json;
            apiRequest.AddBody(ads);
            var response = _client.Execute(apiRequest);
            var data = JsonConvert.DeserializeObject<IEnumerable<GetAdsResponse>>(response.Content);
            return data;
        }
    }
}
