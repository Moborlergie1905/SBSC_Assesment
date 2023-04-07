using MediatR;

namespace WalletSolution.Application.WalletUsers.Command;
public class AddUserCommand : IRequest<int>
{   
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProfilePicture { get; set; }
}
