using Newtonsoft.Json;
using RestSharp;

namespace GoogleAPICore.API
{
    public class EncryptionAPI
    {
        public static RestClient _client;

        public static string EncryptPassword(string password, string baseUrl)
        {
            _client = new RestClient(baseUrl);
            var apiRequest = new RestRequest(Method.GET);
            apiRequest.Resource = "/EncryptionApi/EncryptPassword";
            apiRequest.AddParameter("password", password);
            var response = _client.Execute(apiRequest);
            var data = JsonConvert.DeserializeObject<string>(response.Content);
            return data;
        }

        public static string DecryptPassword(string password, string baseUrl)
        {
            _client = new RestClient(baseUrl);
            var apiRequest = new RestRequest(Method.GET);
            apiRequest.Resource = "/EncryptionApi/DecryptPassword";
            apiRequest.AddParameter("password", password);
            var response = _client.Execute(apiRequest);
            var data = JsonConvert.DeserializeObject<string>(response.Content);
            return data;
        }
    }
}
