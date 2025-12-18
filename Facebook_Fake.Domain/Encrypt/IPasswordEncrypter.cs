
namespace Facebook_Fake.Domain.Encrypt
{
    public interface IPasswordEncrypter
    {
        string Encrypt(string password);
        bool Verify(string password, string hashedPassword);
    }
}
