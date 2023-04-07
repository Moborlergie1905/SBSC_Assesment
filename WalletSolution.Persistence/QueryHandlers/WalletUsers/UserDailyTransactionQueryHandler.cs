using MediatR;
using Microsoft.EntityFrameworkCore;
using WalletSolution.Application.WalletUsers.Query;
using WalletSolution.Application.WalletUsers.Query.QueryModels;
using WalletSolution.Common.Exceptions;
using WalletSolution.Domain.Entities.WalletUsers;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.QueryHandlers.WalletUsers;
public class UserDailyTransactionQueryHandler : IRequestHandler<GetUserPeriodicTransactionQuery, List<TransactionQueryModel>>
{
    private readonly ApplicationDbContext _context;

    public UserDailyTransactionQueryHandler(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<TransactionQueryModel>> Handle(GetUserPeriodicTransactionQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new InvalidNullInputException(nameof(request));

        var entity = _context.Set<Transaction>();

        var transactions = await entity.AsQueryable()
            .Where(x => x.WalletUserId == request.UserId && x.DateCreated.Date == request.Period.Date)
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
