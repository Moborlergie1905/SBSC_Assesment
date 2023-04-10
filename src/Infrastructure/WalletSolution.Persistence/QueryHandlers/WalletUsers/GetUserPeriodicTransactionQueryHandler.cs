using MediatR;
using WalletSolution.Application.WalletUsers.Query.QueryModels;
using WalletSolution.Application.WalletUsers.Query;
using WalletSolution.Persistence.Data;
using WalletSolution.Common.Exceptions;
using WalletSolution.Domain.Entities.WalletUsers;
using Microsoft.EntityFrameworkCore;

namespace WalletSolution.Persistence.QueryHandlers.WalletUsers;
public class GetUserPeriodicTransactionQueryHandler : IRequestHandler<GetUserPeriodicTransactionQuery, List<UserTransactionQueryModel>>
{
    private readonly ApplicationDbContext _context;

    public GetUserPeriodicTransactionQueryHandler(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<UserTransactionQueryModel>> Handle(GetUserPeriodicTransactionQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new InvalidNullInputException(nameof(request));

        var entity = _context.Set<Transaction>();
        string[] periods = { "Day", "Week", "Month", "Year" };
        var transactions = entity.AsQueryable();
        var userTransactions =  transactions.Where(x => x.WalletUserId == request.UserId);

        //if (userTransactions.Count() == 0)
        //    throw new NotFoundException("User not found");
        if (!periods.Contains(request.Period))
            throw new InvalidOperationException("Period must be one of [Day, Week, Month, Year]");

        if (request.Period == "Day")
        {
            userTransactions = userTransactions
                .Where(x => x.DateCreated.Date == request.SelectedDate.Date);
        }
        if (request.Period == "Week")
        {
            DayOfWeek currentDay = request.SelectedDate.DayOfWeek;
            int daysTillCurrentDay = currentDay - DayOfWeek.Sunday;
            DateTime weekStart = request.SelectedDate.AddDays(-daysTillCurrentDay);
            DateTime weekEnd = request.SelectedDate;
            userTransactions = userTransactions
                .Where(x => x.DateCreated.Date >= weekStart.Date && x.DateCreated.Date <= weekEnd.Date);               
        }
        if (request.Period == "Month")
        {
            userTransactions = userTransactions
                .Where(x => x.DateCreated.Month == request.SelectedDate.Month);               
        }
        if (request.Period == "Year")
        {
            userTransactions = userTransactions
                .Where(x => x.DateCreated.Year == request.SelectedDate.Year);                
        }

        var userTrans = await userTransactions
            .Select(x => new UserTransactionQueryModel
            {
                TransactionDate = x.DateCreated,
                TransactionType = x.TransactionType,
                Amount = x.Amount,
                Currency = x.Currency
            }).ToListAsync();

        return userTrans;
    }
}
