using MediatR;
using WalletSolution.Application.WalletUsers.Query.QueryModels;

namespace WalletSolution.Application.WalletUsers.Query;
public class GetUserTransactionListQuery : IRequest<List<UserTransactionQueryModel>>
{
    public Guid UserId { get; set; }
}
