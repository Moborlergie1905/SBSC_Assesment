using MediatR;
using WalletSolution.Domain.Enums;

namespace WalletSolution.Application.WalletUsers.Command;
public class AddTransactionCommand : IRequest<int>
{
    public Guid WalletUserId { get; set; }
    public TransactionType TransactionType { get; set; }
    public decimal Amount { get; set; }
}
