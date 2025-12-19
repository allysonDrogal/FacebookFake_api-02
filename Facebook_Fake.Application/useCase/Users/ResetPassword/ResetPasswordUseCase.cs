using Facebook_Fake.Communication.Requests;
using Facebook_Fake.Communication.Responses;
using Facebook_Fake.Domain.Encrypt;
using Facebook_Fake.Domain.Repositories.Users;

namespace Facebook_Fake.Application.useCase.Users.ResetPassword
{
    public class ResetPasswordUseCase : IResetPasswordUseCase
    {
        private readonly IPasswordResetTokenRepository _tokenRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordEncrypter _passwordEncrypter;

        public ResetPasswordUseCase(
            IPasswordResetTokenRepository tokenRepository,
            IUsersRepository usersRepository,
            IPasswordEncrypter passwordEncrypter)
        {
            _tokenRepository = tokenRepository;
            _usersRepository = usersRepository;
            _passwordEncrypter = passwordEncrypter;
        }

        public async Task<ResponseForgotPasswordJson> Execute(RequestResetPasswordJson request)
        {
            var resetToken = await _tokenRepository.GetByTokenAsync(request.Token);

            if (resetToken == null)
            {
                throw new UnauthorizedAccessException("Token inválido ou expirado.");
            }

            // Atualizar senha do usuário
            var user = resetToken.User;
            user.Password = _passwordEncrypter.Encrypt(request.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;

            await _usersRepository.UpdateAsync(user);

            // Marcar token como usado
            resetToken.IsUsed = true;
            await _tokenRepository.UpdateAsync(resetToken);

            return new ResponseForgotPasswordJson
            {
                Message = "Senha redefinida com sucesso!"
            };
        }
    }
}