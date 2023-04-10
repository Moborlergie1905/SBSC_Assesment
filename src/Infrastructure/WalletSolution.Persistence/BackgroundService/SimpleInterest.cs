using Microsoft.EntityFrameworkCore;
using WalletSolution.Domain.Entities.WalletUsers;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.BackgroundService;
public class SimpleInterest
{
    private readonly ApplicationDbContext _context;

    public SimpleInterest(ApplicationDbContext context)
    {
        _context = context;
    }
    public void CalculateInterest()
    {
        DateTime currentYear = DateTime.Now;
        decimal rate = 3.75M;
        var wallets = _context.Set<Wallet>().AsQueryable();
        var qualifiedWallets = wallets
            .Where(x => currentYear.Subtract(x.DateCreated).Days / 365  >= 1 && x.InterestDueYear < currentYear.Year)
            .ExecuteUpdate(p => p
            .SetProperty(x => x.Balance , x => x.Balance + (x.Balance * rate/100))
            .SetProperty(x => x.InterestDueYear, x => currentYear.Year));
       
    }
}
