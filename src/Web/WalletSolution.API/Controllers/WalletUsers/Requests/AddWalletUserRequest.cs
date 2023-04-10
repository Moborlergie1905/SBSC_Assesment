using Microsoft.AspNetCore.Http;

namespace WalletSolution.API.Controllers.WalletUsers.Requests;
public class AddWalletUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IFormFile File { get; set; }    
}
