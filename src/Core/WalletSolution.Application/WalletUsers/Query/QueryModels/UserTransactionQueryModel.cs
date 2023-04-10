using WalletSolution.Domain.Enums;

namespace WalletSolution.Application.WalletUsers.Query.QueryModels;
public class UserTransactionQueryModel
{
    public DateTime TransactionDate { get; set; }
    public TransactionType TransactionType { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
}
