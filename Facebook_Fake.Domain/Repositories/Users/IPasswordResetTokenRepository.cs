
using Facebook_Fake.Domain.Entities;

namespace Facebook_Fake.Domain.Repositories.Users
{
    public interface IPasswordResetTokenRepository
    {
        Task AddAsync(PasswordResetToken token);
        Task<PasswordResetToken?> GetByTokenAsync(string token);
        Task UpdateAsync(PasswordResetToken token);
        Task DeleteExpiredTokensAsync(Guid userId);
    }
}
