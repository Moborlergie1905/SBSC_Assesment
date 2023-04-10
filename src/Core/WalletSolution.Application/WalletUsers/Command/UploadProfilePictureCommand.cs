using MediatR;

namespace WalletSolution.Application.WalletUsers.Command;
public class UploadProfilePictureCommand : IRequest<int>
{
    public string Email { get; set; }
    public string ProfilePicture { get; set; }
}
