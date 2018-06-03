using GoogleAPICore.Models;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace GoogleAPICore.API
{
    public class LoginAPI
    {
        public static RestClient _client;

        public static void SaveCredentials(OAuthLogin oauthLogin,string baseUrl)
        {
            _client = new RestClient(baseUrl);
            var apiRequest = new RestRequest(Method.POST);
            apiRequest.Resource = "/CrendentialsApi/SaveCredentials";
            apiRequest.RequestFormat = DataFormat.Json;
            apiRequest.AddBody(oauthLogin);
            var response = _client.Execute(apiRequest);
        }

        public static OAuthLogin CheckUserExists(string email, string baseUrl)
        {
            var result = new OAuthLogin();

            try
            {
                _client = new RestClient(baseUrl);
                var apiRequest = new RestRequest(Method.GET);
                apiRequest.Resource = "/CrendentialsApi/GetCredential";
                apiRequest.AddParameter("email", email);
                var response = _client.Execute(apiRequest);
                result = JsonConvert.DeserializeObject<OAuthLogin>(response.Content);

                return result;
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }

            
        }
    }
}
