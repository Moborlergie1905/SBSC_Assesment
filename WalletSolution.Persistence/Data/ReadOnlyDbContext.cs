using Microsoft.EntityFrameworkCore;

namespace WalletSolution.Persistence.Data;

public class ReadOnlyDbContext : ApplicationDbContext
{
    public ReadOnlyDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {        
    }
}
