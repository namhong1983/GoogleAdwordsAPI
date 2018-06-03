using GoogleAPICore.Models;
using GoogleAPICore.Persistence.Repository.Interface;
using System;
using System.Linq;

namespace GoogleAPICore.Persistence.Repository
{
    public class CredentialsRepository: ICredentialsRepository
    {
        private readonly Context _context;

        public CredentialsRepository(Context context)
        {
            _context = context;
        }

        public OAuthLogin GetCredentialByEmail(string email)
        {
            try
            {
                var result = _context.OAuthLogin.FirstOrDefault(s => s.Email == email);
                return result;
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }      
        }

        public void SaveLoginCredentials(OAuthLogin oauthLogin)
        {
            try
            {
                _context.OAuthLogin.Add(oauthLogin);
                _context.SaveChanges();
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
        }
    }
}
