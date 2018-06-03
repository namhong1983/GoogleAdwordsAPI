using Google.Api.Ads.AdWords.Lib;
using GoogleAPICore.Controllers.Resources;
using GoogleAPICore.Models;

namespace GoogleAPICore.Persistence.Repository.Interface
{
    public interface IOAuthRepository
    {
        void DoAuth2Authorization(AdWordsUser user, string authorizationCode);
        AdWordsUser GetConfiguration();
        GoogleUserDataResponse GetGoogleUserData(string access_token);
        GetTokenResponse AuthenticateUser();
        string LoginUsingGoogle();
        string ExternalLoginToGoogle(string baseUrl);
    }
}
