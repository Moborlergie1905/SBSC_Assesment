using Microsoft.EntityFrameworkCore;
using WalletSolution.Domain.Entities;
using WalletSolution.Common.Utilities;
using WalletSolution.Domain.Entities.Admins;

namespace WalletSolution.Persistence.Data;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public ApplicationDbContext() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CurrencyType>()
            .HasData(
            new CurrencyType { Id = 1, Currency = "US Dollar", CurrencyCode = "USD" },
            new CurrencyType { Id = 2, Currency = "Great Britain Pounds", CurrencyCode = "GBP" },
            new CurrencyType { Id = 3, Currency = "Naira", CurrencyCode = "NGN" });
        modelBuilder.Entity<AppUser>()
            .HasData(
                new AppUser { Id = 1, Email = "test.admin@wallet.com", Password = SecurityHelper.GetSha256Hash("password123") }
            );
         modelBuilder.Entity<Role>()
            .HasData(
                new Role { Id = 1, RoleName = "SuperAdmin" },
                new Role { Id = 2, RoleName = "Admin" },                
                new Role { Id = 3, RoleName = "WalletUser" }
            );
        modelBuilder.Entity<UserRole>()
            .HasData(new UserRole { Id = 1, AppUserId = 1, RoleId = 1 });

        var entitiesAssembly = typeof(IEntity).Assembly;

        modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IEntity).Assembly);
        modelBuilder.AddPluralizingTableNameConvention();
    }

    public async Task<int> ExecuteSqlRawAsync(string query, CancellationToken cancellationToken)
    {
        var result = await base.Database.ExecuteSqlRawAsync(query, cancellationToken);
        return result;
    }
    public async Task<int> ExecuteSqlRawAsync(string query) => await ExecuteSqlRawAsync(query, CancellationToken.None);
}
