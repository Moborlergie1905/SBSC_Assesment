using MediatR;
using Microsoft.EntityFrameworkCore;
using WalletSolution.Application.WalletUsers.Query;
using WalletSolution.Application.WalletUsers.Query.QueryModels;
using WalletSolution.Domain.Entities.WalletUsers;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.QueryHandlers.WalletUsers;
public class GetUserTransactionQueryHandler : IRequestHandler<GetUserTransactionListQuery, List<UserTransactionQueryModel>>
{
    private readonly ApplicationDbContext _context;

    public GetUserTransactionQueryHandler(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<UserTransactionQueryModel>> Handle(GetUserTransactionListQuery request, CancellationToken cancellationToken)
    {
        var transactions = await _context.Set<Transaction>()
            .Where(x => x.WalletUserId == request.UserId)
            .Select(x => new UserTransactionQueryModel
            {
                TransactionDate = x.DateCreated,
                TransactionType = x.TransactionType,
                Amount = x.Amount,
                Currency = x.Currency
            })
            .ToListAsync();       
        
        return transactions;
    }
}
