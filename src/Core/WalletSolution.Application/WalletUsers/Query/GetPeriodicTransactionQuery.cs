using MediatR;
using WalletSolution.Application.WalletUsers.Query.QueryModels;

namespace WalletSolution.Application.WalletUsers.Query;
public class GetPeriodicTransactionQuery : IRequest<List<TransactionQueryModel>>
{
    public DateTime SelectedDate { get; set; }
    public string Period { get; set; }
}
