using MediatR;
using WalletSolution.Application.WalletUsers.Query.QueryModels;

namespace WalletSolution.Application.WalletUsers.Query;
public class GetWalletUserByEmailQuery : IRequest<WalletUserQueryModel>
{
    public string Email { get; set; }
}
