using WalletSolution.Domain.Enums;

namespace WalletSolution.Application.WalletUsers.Query.QueryModels;
public class WalletUserQueryModel
{
    public Guid Id { get; set; }
    public string Email { get; set; }   
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserStatus Status { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
    public string ProfilePicture { get; set; }
}
