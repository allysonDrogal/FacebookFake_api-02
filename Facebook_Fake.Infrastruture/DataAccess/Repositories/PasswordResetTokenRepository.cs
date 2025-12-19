using Facebook_Fake.Domain.Entities;
using Facebook_Fake.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace Facebook_Fake.Infrastruture.DataAccess.Repositories
{
    internal class PasswordResetTokenRepository : IPasswordResetTokenRepository
    {
        private readonly FacebookDbContext _context;

        public PasswordResetTokenRepository(FacebookDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PasswordResetToken token)
        {
            await _context.PasswordResetTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task<PasswordResetToken?> GetByTokenAsync(string token)
        {
            return await _context.PasswordResetTokens
                .Include(p => p.User)
                .FirstOrDefaultAsync(t => t.Token == token && !t.IsUsed && t.ExpiresAt > DateTime.UtcNow);
        }

        public async Task UpdateAsync(PasswordResetToken token)
        {
            _context.PasswordResetTokens.Update(token);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExpiredTokensAsync(Guid userId)
        {
            var expiredTokens = await _context.PasswordResetTokens
                .Where(p => p.UserId == userId && (p.IsUsed || p.ExpiresAt <= DateTime.UtcNow))
                .ToListAsync();

            _context.PasswordResetTokens.RemoveRange(expiredTokens);
            await _context.SaveChangesAsync();
        }
    }
}
