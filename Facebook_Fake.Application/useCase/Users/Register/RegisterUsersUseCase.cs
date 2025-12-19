using Facebook_Fake.Communication.Requests;
using Facebook_Fake.Communication.Responses;
using Facebook_Fake.Domain.Encrypt;
using Facebook_Fake.Domain.Entities;
using Facebook_Fake.Domain.Repositories.Users;



namespace Facebook_Fake.Application.useCase.Users.Register
{
    public class RegisterUsersUseCase : IRegisterUsersUseCase
    {
        private readonly IUsersRepository _repository;
        private readonly IPasswordEncrypter _passwordEncrypter;

        public RegisterUsersUseCase(IUsersRepository repository, IPasswordEncrypter passwordEncrypter)
        {
            _repository = repository;
            _passwordEncrypter = passwordEncrypter;
        }
        public async Task<ResponseUsersJson> Execute(RequestUsersJson request)
        {
            // 1. Validar a requisição
            Validate(request);

            // criptografar a senha
            var encryptedPassword = _passwordEncrypter.Encrypt(request.Password);


            var user = new Domain.Entities.Users
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Username = request.Username,
                Email = request.Email,
                Password = encryptedPassword, // Senha criptografada
                Gender = request.Gender,
                Birthday = request.Birthday,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // 3. Salvar no banco de dados
             _repository.Add(user);

            // 4. Retornar a resposta com os dados do usuário criado
            return new ResponseUsersJson()
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
            };
        }

        private void Validate(RequestUsersJson request)
        {
            // Adicione suas validações aqui, por exemplo:
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Name is required");

            if (string.IsNullOrWhiteSpace(request.Email))
                throw new ArgumentException("Email is required");

            if (string.IsNullOrWhiteSpace(request.Password))
                throw new ArgumentException("Password is required");

            if (request.Password.Length < 6)
                throw new ArgumentException("Password must be at least 6 characters");
        }
    }
}
