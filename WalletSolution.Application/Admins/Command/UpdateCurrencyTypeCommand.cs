using MediatR;

namespace WalletSolution.Application.Admins.Command;
public class UpdateCurrencyTypeCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Currency { get; set; }
    public string CurrencyCode { get; set; }
    public string CurrencyLogo { get; set; }
}
