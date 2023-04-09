using WalletSolution.Domain.Enums;

namespace WalletSolution.API.Controllers.Admins.Requests;
public class UpdateUserStatusRequest
{
    public Guid UserId { get; set; }
    public UserStatus UserStatus { get; set; }
}
