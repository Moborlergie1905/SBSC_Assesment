using MediatR;

namespace WalletSolution.Application.WalletUsers.Command;
public class FundWalletBalanceCommand : IRequest<int>
{
    public int Id { get; set; }
    public int CurrencyTypeId { get; set; }
    public Guid WalletUserId { get; set; }
    public string Currency { get; set; }
    public decimal Amount { get; set; }
}
