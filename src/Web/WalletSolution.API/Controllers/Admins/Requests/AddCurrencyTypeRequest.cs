namespace WalletSolution.API.Controllers.Admins.Requests;
public class AddCurrencyTypeRequest
{
    public string Currency { get; set; }
    public string CurrencyCode { get; set; }
    public IFormFile File { get; set; }
}
