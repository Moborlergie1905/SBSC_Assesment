using Microsoft.EntityFrameworkCore;
using WalletSolution.Domain.Entities;
using WalletSolution.Common.Utilities;

namespace WalletSolution.Persistence.Data;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public ApplicationDbContext() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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
