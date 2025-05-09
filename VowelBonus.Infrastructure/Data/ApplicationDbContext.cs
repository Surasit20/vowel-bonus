using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VowelBonus.Infrastructure.Data.Configurations;
namespace VowelBonus.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}

    // Register configurations
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new VowelBonusScoreHistoryConfiguration());
    }

    public DbSet<User> User => Set<User>();
    public DbSet<VowelBonusScoreHistory> VowelBonusScoreHistory => Set<VowelBonusScoreHistory>();
}
