using Facebook_Fake.Application.useCase.Users.Register;
using Microsoft.Extensions.DependencyInjection;

namespace Facebook_Fake.Application
{
    public static class DependencyInjectionUsersApplication
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IRegisterUsersUseCase, RegisterUsersUseCase>();
        }

    }
}
