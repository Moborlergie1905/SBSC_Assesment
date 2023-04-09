using System.ComponentModel.DataAnnotations;
using WalletSolution.Domain.Enums;

namespace WalletSolution.Domain.Entities.WalletUsers;
public class WalletUser : BaseEntity<Guid>
{
    [Required, DataType(DataType.EmailAddress)]
    public string Email { get; set; }    
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public UserStatus Status { get; set; } = UserStatus.Active;
    public string ProfilePicture { get; set; }
    public ICollection<Wallet> Wallets { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}
