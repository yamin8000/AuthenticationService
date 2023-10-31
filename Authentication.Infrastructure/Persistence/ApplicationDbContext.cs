using Microsoft.EntityFrameworkCore;
using Authentication.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Authentication.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserChannel> UserChannels { get; set; }
    public DbSet<Verification> Verifications { get; set; }
    public DbSet<Login> Logins { get; set; }
    public DbSet<PasswordReset> PasswordResets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        optionsBuilder.UseNpgsql(configuration.GetSection("ConnectionStrings")["Default"]);
    }
}