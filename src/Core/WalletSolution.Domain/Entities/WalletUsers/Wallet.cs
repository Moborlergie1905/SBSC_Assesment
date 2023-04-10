namespace WalletSolution.Domain.Entities.WalletUsers;
public class Wallet : BaseEntity
{
    public int CurrencyTypeId { get; set; }
    public Guid WalletUserId { get; set; }
    public decimal Balance { get; set; }
    public string Currency { get; set; }
    public int InterestDueYear { get; set; }
    public WalletUser User { get; set; }
}
