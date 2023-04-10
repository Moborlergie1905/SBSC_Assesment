using MediatR;
using Microsoft.EntityFrameworkCore;
using WalletSolution.Application.WalletUsers.Command;
using WalletSolution.Common.Exceptions;
using WalletSolution.Domain.Entities.WalletUsers;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.CommandHandlers.WalletUsers;
public class AddWalletCommandHandler : IRequestHandler<AddWalletCommand, int>
{
    private readonly ApplicationDbContext _context;

    public AddWalletCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(AddWalletCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new InvalidNullInputException(nameof(request));
        var entity = _context.Set<Wallet>();
        var existingCurrency = entity
            .FirstOrDefaultAsync(x => x.WalletUserId == request.WalletUserId && x.CurrencyTypeId == request.CurrencyTypeId);
        if (existingCurrency is not null)
            throw new ExistingRecordException("Selected currency is already added to your wallet");
        var wallet = new Wallet
        {
            WalletUserId = request.WalletUserId,
            CurrencyTypeId = request.CurrencyTypeId,
            Currency = request.Currency,
            Balance = 0
        };
        entity.Add(wallet);
        int result = await _context.SaveChangesAsync();
        if(result < 1)
            throw new InsertUpdateException("Wallet", "Insert");
        return result;
    }
}
