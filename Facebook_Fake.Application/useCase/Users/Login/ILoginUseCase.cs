using Facebook_Fake.Communication.Requests;
using Facebook_Fake.Communication.Responses;

namespace Facebook_Fake.Application.useCase.Users.Login
{
    public interface ILoginUseCase
    {
        Task<ResponseLoginJson> Execute(RequestLoginJson request);
    }
}
