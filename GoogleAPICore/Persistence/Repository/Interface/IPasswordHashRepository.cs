namespace GoogleAPICore.Persistence.Repository.Interface
{
    public interface IPasswordHashRepository
    {
        string EncryptString(string password);
        string DecryptString(string encryptedPassword);
    }
}
