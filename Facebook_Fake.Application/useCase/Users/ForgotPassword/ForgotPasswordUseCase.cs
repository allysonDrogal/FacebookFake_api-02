
using Facebook_Fake.Communication.Requests;
using Facebook_Fake.Communication.Responses;
using Facebook_Fake.Domain.Entities;
using Facebook_Fake.Domain.Repositories.Users;
using Facebook_Fake.Domain.Services;
using System.Security.Cryptography;

namespace Facebook_Fake.Application.useCase.Users.ForgotPassword
{
    public class ForgotPasswordUseCase : IForgotPasswordUseCase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordResetTokenRepository _tokenRepository;
        private readonly IEmailService _emailService;

        public ForgotPasswordUseCase(
            IUsersRepository usersRepository,
            IPasswordResetTokenRepository tokenRepository,
            IEmailService emailService)
        {
            _usersRepository = usersRepository;
            _tokenRepository = tokenRepository;
            _emailService = emailService;
        }

        public async Task<ResponseForgotPasswordJson> Execute(RequestForgotPasswordJson request)
        {
            var user = await _usersRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                // Por segurança, não informamos se o email existe ou não
                return new ResponseForgotPasswordJson
                {
                    Message = "Se o email existir, um link de redefinição será enviado."
                };
            }

            // Deletar tokens antigos/expirados
            await _tokenRepository.DeleteExpiredTokensAsync(user.Id);

            // Gerar token único
            var token = GenerateSecureToken();

            var resetToken = new PasswordResetToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                IsUsed = false,
                CreatedAt = DateTime.UtcNow
            };

            await _tokenRepository.AddAsync(resetToken);

            // Enviar email
            await _emailService.SendPasswordResetEmailAsync(user.Email, user.Name, token);

            return new ResponseForgotPasswordJson
            {
                Message = "Se o email existir, um link de redefinição será enviado."
            };
        }

        private string GenerateSecureToken()
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes).Replace("+", "-").Replace("/", "_").Replace("=", "");
        }
    }
}
