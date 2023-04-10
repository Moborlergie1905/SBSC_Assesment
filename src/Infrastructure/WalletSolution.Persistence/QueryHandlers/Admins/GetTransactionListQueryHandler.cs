using MediatR;
using Microsoft.EntityFrameworkCore;
using WalletSolution.Application.WalletUsers.Query;
using WalletSolution.Application.WalletUsers.Query.QueryModels;
using WalletSolution.Domain.Entities.WalletUsers;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.QueryHandlers.Admins;
public class GetTransactionListQueryHandler : IRequestHandler<GetTransactionListQuery, List<TransactionQueryModel>>
{
    private readonly ApplicationDbContext _context;

    public GetTransactionListQueryHandler(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<TransactionQueryModel>> Handle(GetTransactionListQuery request, CancellationToken cancellationToken)
    {
        var transactionQry = _context.Set<Transaction>().AsQueryable();
        var transactions = await transactionQry
            .Include(x => x.User)
            .Select(x => new TransactionQueryModel
            {
                Name = $"{x.User.FirstName} {x.User.LastName}",
                Amount = x.Amount,
                Currency = x.Currency,
                TransactionDate = x.DateCreated,
                TransactionType = x.TransactionType
            }).ToListAsync();
        return transactions;
    }
}
