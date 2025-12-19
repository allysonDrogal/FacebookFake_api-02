
namespace Facebook_Fake.Domain.Repositories.Users
{
    public interface IUsersRepository
    {
        void Add(Domain.Entities.Users users);
        Task<Domain.Entities.Users?> GetByEmailAsync(string email);

        Task UpdateAsync(Domain.Entities.Users user);
    }
}
