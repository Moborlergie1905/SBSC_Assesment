using MediatR;
using Microsoft.EntityFrameworkCore;
using WalletSolution.Application.WalletUsers.Query;
using WalletSolution.Application.WalletUsers.Query.QueryModels;
using WalletSolution.Domain.Entities.WalletUsers;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.QueryHandlers.WalletUsers;
public class GetUserTransactionQueryHandler : IRequestHandler<GetUserTransactionListQuery, List<TransactionQueryModel>>
{
    private readonly ApplicationDbContext _context;

    public GetUserTransactionQueryHandler(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<TransactionQueryModel>> Handle(GetUserTransactionListQuery request, CancellationToken cancellationToken)
    {
        var transactions = await _context.Set<Transaction>()
            .Where(x => x.WalletUserId == request.UserId)
            .ToListAsync();
        List<TransactionQueryModel> transactionQueryModels = new List<TransactionQueryModel>();
        transactions.ForEach(transaction =>
        {
            transactionQueryModels.Add(new TransactionQueryModel
            {
                TransactionDate = transaction.DateCreated,
                TransactionType = transaction.TransactionType,
                Amount = transaction.Amount,
                Currency = transaction.Currency
            });
        });
        
        return transactionQueryModels;
    }
}
