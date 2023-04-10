using WalletSolution.Domain.Enums;

namespace WalletSolution.Domain.Entities.WalletUsers;
public class Transaction : BaseEntity
{
    public Guid WalletUserId { get; set; }
    public TransactionType TransactionType { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public WalletUser User { get; set; }
}
