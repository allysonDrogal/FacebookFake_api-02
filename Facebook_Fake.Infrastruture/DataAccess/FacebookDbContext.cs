
using Facebook_Fake.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Facebook_Fake.Infrastruture.DataAccess;

internal class FacebookDbContext : DbContext
{
    public FacebookDbContext(DbContextOptions<FacebookDbContext> options) : base(options)
    {
    }

    public DbSet<Users> Users { get; set; }
    public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<PasswordResetToken>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PasswordResetToken>()
            .HasIndex(p => p.Token);
    }
}


