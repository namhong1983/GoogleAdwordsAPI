using GoogleAPICore.Persistence.Repository.Interface;
using Microsoft.AspNetCore.DataProtection;

namespace GoogleAPICore.Persistence.Repository
{
    public class PasswordHashRepository: IPasswordHashRepository
    {
        private readonly IDataProtector _protector;

        public PasswordHashRepository(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector(GetType().FullName);
        }

        public string EncryptString(string password)
        {
            var encrypted = _protector.Protect(password);
            return encrypted;
         }

        public string DecryptString(string encryptedPassword)
        {
            var decryptedPassword = _protector.Unprotect(encryptedPassword);
            return decryptedPassword;
        }
    }
}
