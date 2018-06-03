using GoogleAPICore.Models;

namespace GoogleAPICore.Persistence.Repository.Interface
{
    public interface ICredentialsRepository
    {
        void SaveLoginCredentials(OAuthLogin oauthLogin);
        OAuthLogin GetCredentialByEmail(string email);

    }
}
