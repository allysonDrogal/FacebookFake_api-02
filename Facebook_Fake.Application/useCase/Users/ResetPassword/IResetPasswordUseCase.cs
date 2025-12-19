using Facebook_Fake.Communication.Requests;
using Facebook_Fake.Communication.Responses;

namespace Facebook_Fake.Application.useCase.Users.ResetPassword
{
    public interface IResetPasswordUseCase
    {
        Task<ResponseForgotPasswordJson> Execute(RequestResetPasswordJson request);
    }
}
