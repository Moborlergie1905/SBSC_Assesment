using MediatR;

namespace WalletSolution.Application.WalletUsers.Command;
public class AddWalletCommand : IRequest<int>
{
    public int CurrencyTypeId { get; set; }
    public Guid WalletUserId { get; set; } 
    public string Currency { get; set; }
}
