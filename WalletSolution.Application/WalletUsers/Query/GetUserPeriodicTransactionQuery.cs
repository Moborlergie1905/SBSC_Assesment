using MediatR;
using WalletSolution.Application.WalletUsers.Query.QueryModels;

namespace WalletSolution.Application.WalletUsers.Query;
public class GetUserPeriodicTransactionQuery : IRequest<List<TransactionQueryModel>>
{
    public Guid UserId { get; set; }
    public DateTime Period { get; set; }
}
