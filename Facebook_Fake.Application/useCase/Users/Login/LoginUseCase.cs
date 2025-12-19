using Facebook_Fake.Communication.Requests;
using Facebook_Fake.Communication.Responses;
using Facebook_Fake.Domain.Encrypt;
using Facebook_Fake.Domain.Repositories.Users;
using Facebook_Fake.Infrastruture.JwtToken;

namespace Facebook_Fake.Application.useCase.Users.Login
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUsersRepository _repository;
        private readonly IPasswordEncrypter _passwordEncrypter;
        private readonly ITokenGenerator _tokenGenerator;

        public LoginUseCase(
            IUsersRepository usersRepository,
            IPasswordEncrypter passwordEncrypter,
            ITokenGenerator tokenGenerator)
        {
             _repository = usersRepository;
            _passwordEncrypter = passwordEncrypter;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<ResponseLoginJson> Execute(RequestLoginJson request)
        {
            var user = await _repository.GetByEmailAsync(request.Email);

            if (user == null)
                throw new ArgumentException("Invalid email or password");

            var isPasswordValid = _passwordEncrypter
                .Verify(request.Password, user.Password);
            if (!isPasswordValid)
                throw new ArgumentException("Invalid email or password");

            var token = _tokenGenerator.GenerateToken(user.Id, user.Email, user.Name);

            return new ResponseLoginJson()
            {
                Token = token,
                Email = user.Email,
                Name = user.Name,
            };
        }
    }
}
