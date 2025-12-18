
using Facebook_Fake.Domain.Repositories.Users;

namespace Facebook_Fake.Infrastruture.DataAccess.Repositories
{
    internal class UsersRepository : IUsersRepository
    {
        public void Add(Domain.Entities.Users users)
        {
            var dbContext = new FacebookDbContext();

            dbContext.Users.Add(users);

            dbContext.SaveChanges();
        }
    }
}
