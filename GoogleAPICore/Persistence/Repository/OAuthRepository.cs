using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.Common.Lib;
using GoogleAPICore.Controllers.Resources;
using GoogleAPICore.Models;
using GoogleAPICore.Persistence.Repository.Interface;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace GoogleAPICore.Persistence.Repository
{
    public class OAuthRepository : IOAuthRepository
    {
        private readonly AdWordsConfig _config;

        public OAuthRepository(IOptions<AdWordsConfig> config)
        {
            _config = config.Value;
        }

        public void DoAuth2Authorization(AdWordsUser user, string authorizationCode)
        {
            try
            {
                AdsOAuthProviderForApplications oAuth2Provider =
                    (user.OAuthProvider as AdsOAuthProviderForApplications);
             

                // Fetch the access and refresh tokens.
                oAuth2Provider.FetchAccessAndRefreshTokens(authorizationCode);
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
        }

        public string LoginUsingGoogle()
        {
            //string result = "https://accounts.google.com/o/oauth2/v2/auth?" +
            //    "client_id=" + _config.OAuth2ClientId +
            //    "&response_type=code" +
            //    "&scope=openid%20email%20profile" +
            //    "&redirect_uri=" + _config.OAuthRedirectURI +
            //    "&state=offline";

            string result = "https://accounts.google.com/o/oauth2/v2/auth?" +
                "client_id=" + _config.OAuth2ClientId +
                "&response_type=code" +
                "&scope=https://www.googleapis.com/auth/adwords" +
                "&redirect_uri=" + _config.OAuthRedirectURI +
                "&state=offline";
            return result;
        }

        public string ExternalLoginToGoogle(string baseUrl)
        {
            string action = "Login/LoginAccount?";
           string result = baseUrl +action+  "provider=Google&"+
                            "response_type=token&"+
                            "client_id="+ _config.OAuth2ClientId+
                            "&redirect_uri="+_config.OAuthRedirectURI;

            return result;
        }


        public GetTokenResponse AuthenticateUser()
        {
            try
            {
                //string authUrl = "https://accounts.google.com/o/oauth2/v2/auth"; //redirect to this url
                string authUrl = "https://accounts.google.com/o/oauth2/token";
                RestClient _client = new RestClient(authUrl);
                RestRequest request = new RestRequest(Method.POST);
                string code = _config.AuthorizationCode +
                    "&redirect_uri=" + _config.OAuthRedirectURI +
                    "&client_id=" + _config.OAuth2ClientId +
                    "&client_secret=" + _config.OAuth2ClientSecret; //+

                request.AddParameter("client_id", _config.OAuth2ClientId);
                request.AddParameter("client_secret", _config.OAuth2ClientSecret);
               // request.AddParameter("redirect_uri", _config.OAuthRedirectURIPlayground);
                request.AddParameter("refresh_token", _config.OAuth2RefreshToken);
                request.AddParameter("grant_type", "refresh_token");

                var response = _client.Execute(request);
                var result = JsonConvert.DeserializeObject<GetTokenResponse>(response.Content);
                return result;
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
        }


        public GoogleUserDataResponse GetGoogleUserData(string access_token)
        {
            GoogleUserDataResponse result;
            try
            {
                var urlProfile = "https://www.googleapis.com/oauth2/v1/userinfo";
                RestClient _client = new RestClient(urlProfile);
                RestRequest request = new RestRequest();
                string code = _config.AuthorizationCode +
                    "&redirect_uri=" + _config.OAuthRedirectURI +
                    "&client_id=" + _config.OAuth2ClientId +
                    "&client_secret=" + _config.OAuth2ClientSecret +
                    "&grant_type=authorization_code";//+ _config.AuthorizationCode;

                request.AddParameter("access_token", access_token);
                request.AddParameter("code", code);
                request.RequestFormat = DataFormat.Json;
                var response = _client.Execute(request);
                result = JsonConvert.DeserializeObject<GoogleUserDataResponse>(response.Content);

            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }

            return result;
        }

        public AdWordsUser GetConfiguration()
        {
            AdWordsUser user = new AdWordsUser();
            AdWordsAppConfig _configAppReference = new AdWordsAppConfig();
            _configAppReference.DeveloperToken = _config.DeveloperToken;
            _configAppReference.ClientCustomerId = _config.ClientCustomerId;
            _configAppReference.OAuth2RefreshToken = _config.OAuth2RefreshToken;
            _configAppReference.OAuth2Scope = _config.OAuth2Scope;
            _configAppReference.OAuth2AccessToken = _config.OAuth2AccessToken;

            user = new AdWordsUser(_configAppReference);

            user.Config.OAuth2AccessToken = _config.OAuth2AccessToken;
            user.Config.OAuth2ClientId = _config.OAuth2ClientId;
            user.Config.OAuth2ClientSecret = _config.OAuth2ClientSecret;
            user.Config.OAuth2RefreshToken = _config.OAuth2RefreshToken;
            user.Config.OAuth2Scope = _config.OAuth2Scope;

            return user;
        }
    }
}
