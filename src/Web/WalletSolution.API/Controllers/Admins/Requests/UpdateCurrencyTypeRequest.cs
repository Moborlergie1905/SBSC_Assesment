namespace WalletSolution.API.Controllers.Admins.Requests
{
    public class UpdateCurrencyTypeRequest
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyLogo { get; set; }
    }
}
