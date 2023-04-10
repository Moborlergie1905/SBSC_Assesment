using MediatR;
using WalletSolution.Domain.Enums;

namespace WalletSolution.Application.Admins.Command;
public class UpdateUserStatusCommand : IRequest<int>
{
    public Guid UserId { get; set; }
    public UserStatus UserStatus { get; set; }
}
