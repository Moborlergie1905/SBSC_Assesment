using MediatR;
using WalletSolution.Application.WalletUsers.Query.QueryModels;
using WalletSolution.Persistence.Data;
using WalletSolution.Common.Exceptions;
using WalletSolution.Domain.Entities.WalletUsers;
using Microsoft.EntityFrameworkCore;
using WalletSolution.Application.WalletUsers.Query;

namespace WalletSolution.Persistence.QueryHandlers.WalletUsers;
public class UserWeeklyTransactionQueryHandler : IRequestHandler<GetUserPeriodicTransactionQuery, List<UserTransactionQueryModel>>
{
    private readonly ApplicationDbContext _context;

    public UserWeeklyTransactionQueryHandler(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<UserTransactionQueryModel>> Handle(GetUserPeriodicTransactionQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new InvalidNullInputException(nameof(request));

        var entity = _context.Set<Transaction>();

        DayOfWeek currentDay = request.SelectedDate.DayOfWeek;
        int daysTillCurrentDay = currentDay - DayOfWeek.Sunday;
        DateTime weekStart = request.SelectedDate.AddDays(-daysTillCurrentDay);
        DateTime weekEnd = request.SelectedDate;

        var transactions = await entity.AsQueryable()
           .Where(x => x.WalletUserId == request.UserId && (x.DateCreated.Date >= weekStart.Date && x.DateCreated.Date <= weekEnd.Date) )
           .ToListAsync();

        List<UserTransactionQueryModel> transactionQueryModels = new List<UserTransactionQueryModel>();
        transactions.ForEach(transaction =>
        {
            transactionQueryModels.Add(new UserTransactionQueryModel
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
