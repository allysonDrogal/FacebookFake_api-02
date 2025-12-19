
using Facebook_Fake.Domain.Encrypt;
using Facebook_Fake.Domain.Repositories.Users;
using Facebook_Fake.Infrastruture.DataAccess;
using Facebook_Fake.Infrastruture.DataAccess.Repositories;
using Facebook_Fake.Infrastruture.Encrypt;
using Facebook_Fake.Infrastruture.JwtToken;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace Facebook_Fake.Infrastruture
{
    public static class DependencyInjectionUsers
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddRepositories(services);
            AddSecurity(services, configuration);
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("A connection string 'DefaultConnection' não foi encontrada no appsettings.json");
            }

            var version = new Version(9, 4, 0);
            var serverVersion = new MySqlServerVersion(version);

            services.AddDbContext<FacebookDbContext>(options =>
                options.UseMySql(connectionString, serverVersion));
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, UsersRepository>();
        }

        private static void AddSecurity(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPasswordEncrypter, BCryptPasswordEncrypter>();

            var secretKey = configuration.GetValue<string>("Jwt:SecretKey");
            var expiryMinutes = configuration.GetValue<int>("Jwt:ExpirationMinutes");

            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("A chave JWT 'Jwt:SecretKey' não foi encontrada no appsettings.json");
            }

            services.AddScoped<ITokenGenerator>(provider =>
                new JwtTokenGenerator(secretKey, expiryMinutes > 0 ? expiryMinutes : 100));
        }
    }
}
