using Microsoft.EntityFrameworkCore;

namespace WalletSolution.Persistence.Data;
public interface IApplicationDbContext
{
    DbSet<T> Set<T>() where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellation);
    Task<int> ExecuteSqlRawAsync(string query, CancellationToken cancellationToken);
    Task<int> ExecuteSqlRawAsync(string query);
}
