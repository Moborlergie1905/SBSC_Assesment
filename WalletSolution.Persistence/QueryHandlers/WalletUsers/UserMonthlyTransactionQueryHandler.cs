using MediatR;
using WalletSolution.Application.WalletUsers.Query.QueryModels;
using WalletSolution.Persistence.Data;
using WalletSolution.Common.Exceptions;
using WalletSolution.Domain.Entities.WalletUsers;
using Microsoft.EntityFrameworkCore;
using WalletSolution.Application.WalletUsers.Query;

namespace WalletSolution.Persistence.QueryHandlers.WalletUsers;
public class UserMonthlyTransactionQueryHandler : IRequestHandler<GetUserPeriodicTransactionQuery, List<TransactionQueryModel>>
{
    private readonly ApplicationDbContext _context;

    public UserMonthlyTransactionQueryHandler(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<TransactionQueryModel>> Handle(GetUserPeriodicTransactionQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new InvalidNullInputException(nameof(request));

        var entity = _context.Set<Transaction>();

        var transactions = await entity.AsQueryable()
            .Where(x => x.WalletUserId == request.UserId && x.DateCreated.Month == request.Period.Month)
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
