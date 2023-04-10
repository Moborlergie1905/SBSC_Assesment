using MediatR;
using WalletSolution.Application.WalletUsers.Query.QueryModels;

namespace WalletSolution.Application.WalletUsers.Query;
public class GetUserWalletListQuery : IRequest<List<WalletQueryModel>>
{
    public Guid UserId { get; set; }
}
