using Microsoft.EntityFrameworkCore;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence;
public class ApplicationDbContextFactory : DesignTimeDbContextFactoryBase<ApplicationDbContext>
{
    public ApplicationDbContextFactory()
    {}
    protected override ApplicationDbContext CreateNewInstance(DbContextOptions<ApplicationDbContext> options)
    {
        return new ApplicationDbContext(options);
    }
}
