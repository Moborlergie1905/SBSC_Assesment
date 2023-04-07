using MediatR;

namespace WalletSolution.Application.Admins.Command;
public class AddCurrencyTypeCommand : IRequest<int>
{
    public string Currency { get; set; }
    public string CurrencyCode { get; set; }
    public string CurrencyLogo { get; set; }
}
