using MediatR;
using Microsoft.EntityFrameworkCore;
using WalletSolution.Application.Admins.Command;
using WalletSolution.Common.Exceptions;
using WalletSolution.Domain.Entities.Admins;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.CommandHandlers.Admins;
public class AddCurrencyTypeCommandHandler : IRequestHandler<AddCurrencyTypeCommand, int>
{
    private readonly ApplicationDbContext _context;

    public AddCurrencyTypeCommandHandler(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<int> Handle(AddCurrencyTypeCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new InvalidNullInputException(nameof(request));
        var entity = _context.Set<CurrencyType>();
        var existingCurrency = await entity.AsQueryable()
            .FirstOrDefaultAsync(x => x.Currency == request.Currency || x.CurrencyCode == request.CurrencyCode);
        if (existingCurrency is not null)
            throw new ExistingRecordException("This currency alrerady exists");
        var currency = new CurrencyType
        {
            Currency = request.Currency,
            CurrencyCode = request.CurrencyCode,
            CurrencyLogo = request.CurrencyLogo
        };
        entity.Add(currency);
        int result = await _context.SaveChangesAsync();
        if (result < 1)
            throw new InsertUpdateException(nameof(CurrencyType), "Insert");
        return result;
    }
}
