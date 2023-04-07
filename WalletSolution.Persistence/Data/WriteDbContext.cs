using Microsoft.EntityFrameworkCore;

namespace WalletSolution.Persistence.Data;

public class WriteDbContext : ApplicationDbContext
{
    public WriteDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {            
    }
    public WriteDbContext()
    {            
    }
}
