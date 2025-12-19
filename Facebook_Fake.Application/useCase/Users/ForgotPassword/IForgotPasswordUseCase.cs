using Facebook_Fake.Communication.Requests;
using Facebook_Fake.Communication.Responses;


namespace Facebook_Fake.Application.useCase.Users.ForgotPassword
{
    public interface IForgotPasswordUseCase
    {
        Task<ResponseForgotPasswordJson> Execute(RequestForgotPasswordJson request);
    }
}
