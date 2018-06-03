using GoogleAPICore.Controllers.Resources;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace GoogleAPICore.API
{
    public static class AdAccountsAPI
    {
        public static RestClient _client;

        public static IEnumerable<GetAdAccountsResponse> GetAdAccounts(string baseUrl)
        {
            _client = new RestClient(baseUrl);
            var apiRequest = new RestRequest(Method.GET);
            apiRequest.Resource = "/AdAccountsApi/GetAllAdAccount";
            var response = _client.Execute(apiRequest);
            var data = JsonConvert.DeserializeObject<IEnumerable<GetAdAccountsResponse>>(response.Content);
            
            return data;
        }

    }
}
