using MediatR;
using WalletSolution.Application.WalletUsers.Query.QueryModels;

namespace WalletSolution.Application.WalletUsers.Query;
public class LoginQuery : IRequest<UserModel>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
