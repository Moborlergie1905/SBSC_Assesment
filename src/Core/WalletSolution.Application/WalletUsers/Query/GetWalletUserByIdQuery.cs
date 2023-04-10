using MediatR;
using WalletSolution.Application.WalletUsers.Query.QueryModels;

namespace WalletSolution.Application.WalletUsers.Query;
public class GetWalletUserByIdQuery : IRequest<WalletUserQueryModel>
{
    public Guid Id { get; set; }
}
