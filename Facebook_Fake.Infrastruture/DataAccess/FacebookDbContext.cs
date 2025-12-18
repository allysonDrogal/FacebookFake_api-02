
using Facebook_Fake.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Facebook_Fake.Infrastruture.DataAccess;

internal class FacebookDbContext : DbContext
{
    public DbSet<Users> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=localhost;Database=cashflow_db;Uid=face_user;Pwd=123456";


        var serverVersion = new MySqlServerVersion(new Version(9, 4, 0));

        optionsBuilder.UseMySql(connectionString, serverVersion);
    }

}


