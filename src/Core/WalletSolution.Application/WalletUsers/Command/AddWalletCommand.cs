using MediatR;

namespace WalletSolution.Application.WalletUsers.Command;
public class AddWalletCommand : IRequest<int>
{   
    public string Email { get; set; } 
    public string Currency { get; set; }
}
