using Facebook_Fake.Application.useCase.Users.ForgotPassword;
using Facebook_Fake.Application.useCase.Users.Login;
using Facebook_Fake.Application.useCase.Users.Register;
using Facebook_Fake.Application.useCase.Users.ResetPassword;
using Microsoft.Extensions.DependencyInjection;

namespace Facebook_Fake.Application
{
    public static class DependencyInjectionUsersApplication
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IRegisterUsersUseCase, RegisterUsersUseCase>();
            services.AddScoped<ILoginUseCase, LoginUseCase>();
            services.AddScoped<IForgotPasswordUseCase, ForgotPasswordUseCase>();
            services.AddScoped<IResetPasswordUseCase, ResetPasswordUseCase>();
        }

    }
}
