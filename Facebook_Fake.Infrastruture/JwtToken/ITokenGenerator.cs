
namespace Facebook_Fake.Infrastruture.JwtToken
{
    public interface ITokenGenerator
    {
        string GenerateToken(Guid userId, string email, string name);
    }
}
