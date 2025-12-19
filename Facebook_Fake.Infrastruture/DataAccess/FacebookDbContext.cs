
using Facebook_Fake.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Facebook_Fake.Infrastruture.DataAccess;

internal class FacebookDbContext : DbContext
{
    public FacebookDbContext(DbContextOptions<FacebookDbContext> options) : base(options)
    {
    }

    public DbSet<Users> Users { get; set; }
}


