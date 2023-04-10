using MediatR;

namespace WalletSolution.Application.Admins.Command;
public class AssignAdminRoleCommand : IRequest<int>
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
}
