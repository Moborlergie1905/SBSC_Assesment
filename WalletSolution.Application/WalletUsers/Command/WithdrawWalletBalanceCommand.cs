using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletSolution.Application.WalletUsers.Command;
public class WithdrawWalletBalanceCommand : IRequest<int>
{
    public int Id { get; set; }
    public int CurrencyTypeId { get; set; }
    public Guid WalletUserId { get; set; }
    public string Currency { get; set; }
    public decimal Amount { get; set; }
}
