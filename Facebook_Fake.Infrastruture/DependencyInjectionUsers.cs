
using Facebook_Fake.Domain.Encrypt;
using Facebook_Fake.Domain.Repositories.Users;
using Facebook_Fake.Infrastruture.DataAccess;
using Facebook_Fake.Infrastruture.DataAccess.Repositories;
using Facebook_Fake.Infrastruture.Encrypt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Facebook_Fake.Infrastruture
{
    public static class DependencyInjectionUsers
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
          //  AddDbContext(services, configuration);
            AddRepositories(services);
            AddSecurity(services);
        }

        //private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        //{
        //    var connectionString = configuration.GetConnectionString("DefaultConnection");
        //    var version = new Version(9, 4, 0);
        //    var serverVersion = new MySqlServerVersion(version);

        //    services.AddDbContext<FacebookDbContext>(options =>
        //        options.UseMySql(connectionString, serverVersion));
        //}

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, UsersRepository>();
        }

        private static void AddSecurity(IServiceCollection services)
        {
            services.AddScoped<IPasswordEncrypter, BCryptPasswordEncrypter>();
        }
    }
}
