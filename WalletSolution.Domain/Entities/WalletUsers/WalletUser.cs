using WalletSolution.Domain.Enums;

namespace WalletSolution.Domain.Entities.WalletUsers;
public class WalletUser : BaseEntity<Guid>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserStatus Status { get; set; } = UserStatus.Active;
    public string ProfilePicture { get; set; }
    public ICollection<Wallet> Wallets { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}
