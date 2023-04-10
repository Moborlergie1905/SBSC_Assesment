using MediatR;
using WalletSolution.Application.WalletUsers.Query.QueryModels;

namespace WalletSolution.Application.WalletUsers.Query;
public class GetTransactionListQuery : IRequest<List<TransactionQueryModel>>
{
}
