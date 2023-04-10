using MediatR;
using WalletSolution.Application.WalletUsers.Command;
using WalletSolution.Common.Exceptions;
using WalletSolution.Domain.Entities.WalletUsers;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.CommandHandlers.WalletUsers;
public class AddTransactionCommandHandler : IRequestHandler<AddTransactionCommand, int>
{
    private readonly ApplicationDbContext _context;

    public AddTransactionCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new InvalidNullInputException(nameof(request));
        var entity = _context.Set<Transaction>();
        var transaction = new Transaction
        {
            WalletUserId = request.WalletUserId,
            TransactionType = request.TransactionType,
            Amount = request.Amount,
        };
        entity.Add(transaction);
        int result = await _context.SaveChangesAsync();
        if(result < 1)
            throw new InsertUpdateException("Transaction", "Insert");
        return result;
    }
}
