
using Facebook_Fake.Domain.Entities;
using Facebook_Fake.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace Facebook_Fake.Infrastruture.DataAccess.Repositories
{
    internal class UsersRepository : IUsersRepository
    {
        private readonly FacebookDbContext _context;

        public UsersRepository(FacebookDbContext context)
        {
            _context = context;
        }
        public void Add(Domain.Entities.Users users)
        {
            _context.Users.Add(users);
            _context.SaveChanges();
        }

        public async Task<Users?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task UpdateAsync(Users user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
