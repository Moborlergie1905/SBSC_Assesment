namespace WalletSolution.API.Controllers.WalletUsers.Requests;
public class FundOrWithrawRequest
{
    public int Id { get; set; }
    public int CurrencyTypeId { get; set; }
    public Guid WalletUserId { get; set; }
    public string Currency { get; set; }
    public decimal Amount { get; set; }
}
