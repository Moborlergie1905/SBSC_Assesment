using MediatR;
using Microsoft.EntityFrameworkCore;
using WalletSolution.Application.WalletUsers.Command;
using WalletSolution.Common.Exceptions;
using WalletSolution.Domain.Entities.WalletUsers;
using WalletSolution.Domain.Enums;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.CommandHandlers.WalletUsers;
public class FundWalletCommandHandler : IRequestHandler<FundWalletBalanceCommand, int>
{
    private readonly ApplicationDbContext _context;

    public FundWalletCommandHandler(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<int> Handle(FundWalletBalanceCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new InvalidNullInputException(nameof(request));
        var entity = _context.Set<Wallet>();
        var wallet = await entity.FindAsync(request.Id, cancellationToken);
        wallet.Balance += request.Amount;
        entity.Update(wallet);
        int result = await _context.SaveChangesAsync();
        if (result < 1)
            throw new InsertUpdateException("Wallet", "Update");

        var transaction = new Transaction
        {
            WalletUserId = request.WalletUserId,
            TransactionType = TransactionType.Credit,
            Amount = request.Amount,
            Currency = request.Currency,
        };

        _context.Set<Transaction>().Add(transaction);
        _context.SaveChanges();

        return result;      

    }
}
