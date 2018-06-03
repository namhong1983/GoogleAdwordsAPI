using GoogleAPICore.Controllers.Resources;
using Newtonsoft.Json;
using RestSharp;

namespace GoogleAPICore.API
{
    public class OAuthAPI
    {
        public static RestClient _client;

        public static GetTokenResponse GenerateToken(string baseUrl)
        {
            _client = new RestClient(baseUrl);
            var apiRequest = new RestRequest(Method.POST);
            apiRequest.Resource = "/OAuthApi/AuthenticateClient";
            var response = _client.Execute(apiRequest);
            var data = JsonConvert.DeserializeObject<GetTokenResponse>(response.Content);
            return data;
        }

        public static GoogleUserDataResponse GetGoogleUserData(string access_token, string baseUrl)
        {
            _client = new RestClient(baseUrl);
            var apiRequest = new RestRequest(Method.GET);
            apiRequest.Resource = "/OAuthApi/GoogleUserData";
            apiRequest.RequestFormat = DataFormat.Json;
            apiRequest.AddParameter("access_token",access_token);
            var response = _client.Execute(apiRequest);
            var data = JsonConvert.DeserializeObject<GoogleUserDataResponse>(response.Content);
            return data;
        }

        public static string ExternalLogin(string baseUrl, string baseUrlApi)
        {
            _client = new RestClient(baseUrlApi);
            var apiRequest = new RestRequest(Method.GET);
            apiRequest.Resource = "/OAuthApi/ExternalLogin";
            apiRequest.RequestFormat = DataFormat.Json;
            apiRequest.AddParameter("baseUrl", baseUrl);
            var response = _client.Execute(apiRequest);
            var data = JsonConvert.DeserializeObject<string>(response.Content);
            return data;
        }

        public static string GetGoogleLogin(string baseUrl)
        {
            _client = new RestClient(baseUrl);
            var apiRequest = new RestRequest(Method.GET);
            apiRequest.Resource = "/OAuthApi/LoginInGoogle";
            var response = _client.Execute(apiRequest);
            var data = JsonConvert.DeserializeObject<string>(response.Content);
            return data;
        }
    }
}
