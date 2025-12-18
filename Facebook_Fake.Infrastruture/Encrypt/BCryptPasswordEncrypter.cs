using Facebook_Fake.Domain.Encrypt;
using BC = BCrypt.Net.BCrypt;

namespace Facebook_Fake.Infrastruture.Encrypt
{
    public class BCryptPasswordEncrypter : IPasswordEncrypter
    {
        public string Encrypt(string password)
        {
            return BC.HashPassword(password);
        }

        public bool Verify(string password, string hashedPassword)
        {
            return BC.Verify(password, hashedPassword);
        }
    }
}
