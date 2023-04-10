using MediatR;
using WalletSolution.Application.WalletUsers.Query.QueryModels;

namespace WalletSolution.Application.WalletUsers.Query;
public class GetUserPeriodicTransactionQuery : IRequest<List<UserTransactionQueryModel>>
{
    public Guid UserId { get; set; }
    public DateTime SelectedDate { get; set; }
    public string Period { get; set; }
}
