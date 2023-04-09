using MediatR;
using Microsoft.EntityFrameworkCore;
using WalletSolution.Application.WalletUsers.Query;
using WalletSolution.Application.WalletUsers.Query.QueryModels;
using WalletSolution.Common.Exceptions;
using WalletSolution.Domain.Entities.WalletUsers;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.QueryHandlers.Admins;
public class GetPeriodicTransactionsListQueryHandler : IRequestHandler<GetPeriodicTransactionQuery, List<TransactionQueryModel>>
{
    private readonly ApplicationDbContext _context;

    public GetPeriodicTransactionsListQueryHandler(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<TransactionQueryModel>> Handle(GetPeriodicTransactionQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new InvalidNullInputException(nameof(request));

        var entity = _context.Set<Transaction>();
        string[] periods = { "Day", "Week", "Month", "Year" };

        if (!periods.Contains(request.Period))
            throw new InvalidOperationException("Period must be one of [Day, Week, Month, Year]");

        var transactionsQry = entity.AsQueryable();

         transactionsQry = transactionsQry
           .Include(x => x.User);

        if(request.Period == "Day")
            transactionsQry = transactionsQry
                .Where(x => x.DateCreated.Date == request.SelectedDate.Date);

        if (request.Period == "Week")
        {
            DayOfWeek currentDay = request.SelectedDate.DayOfWeek;
            int daysTillCurrentDay = currentDay - DayOfWeek.Sunday;
            DateTime weekStart = request.SelectedDate.AddDays(-daysTillCurrentDay);
            DateTime weekEnd = request.SelectedDate;

            transactionsQry = transactionsQry
               .Where(x => x.DateCreated.Date >= weekStart.Date && x.DateCreated.Date <= weekEnd.Date);

        }
        if(request.Period == "Month")
            transactionsQry = transactionsQry
                .Where(x => x.DateCreated.Month == request.SelectedDate.Month);

        if(request.Period == "Year")
            transactionsQry = transactionsQry
                .Where(x => x.DateCreated.Year == request.SelectedDate.Year);

        var transactions = await transactionsQry
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
