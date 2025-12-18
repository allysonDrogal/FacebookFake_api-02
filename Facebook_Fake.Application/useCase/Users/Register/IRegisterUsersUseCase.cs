using Facebook_Fake.Communication.Requests;
using Facebook_Fake.Communication.Responses;

namespace Facebook_Fake.Application.useCase.Users.Register;

public interface IRegisterUsersUseCase
{
    Task<ResponseUsersJson> Execute(RequestUsersJson request);
}
