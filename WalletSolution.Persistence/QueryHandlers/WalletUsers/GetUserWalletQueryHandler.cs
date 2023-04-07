using MediatR;
using Microsoft.EntityFrameworkCore;
using WalletSolution.Application.WalletUsers.Query;
using WalletSolution.Application.WalletUsers.Query.QueryModels;
using WalletSolution.Domain.Entities.WalletUsers;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.QueryHandlers.WalletUsers;
public class GetUserWalletQueryHandler : IRequestHandler<GetUserWalletListQuery, List<WalletQueryModel>>
{
    private readonly ApplicationDbContext _context;

    public GetUserWalletQueryHandler(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<WalletQueryModel>> Handle(GetUserWalletListQuery request, CancellationToken cancellationToken)
    {
        var wallets = await _context.Set<Wallet>().Where(x => x.WalletUserId == request.UserId).ToListAsync();
        List<WalletQueryModel> walletQueryModels = new List<WalletQueryModel>();
        wallets.ForEach(wallet =>
        {
            walletQueryModels.Add(new WalletQueryModel
            {
                Currency = wallet.Currency,
                Balance = wallet.Balance
            });
        });
        return walletQueryModels;
    }
}
