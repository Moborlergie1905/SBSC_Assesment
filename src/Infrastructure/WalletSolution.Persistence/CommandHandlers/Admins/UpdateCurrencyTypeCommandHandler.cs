using MediatR;
using WalletSolution.Application.Admins.Command;
using WalletSolution.Common.Exceptions;
using WalletSolution.Domain.Entities.Admins;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.CommandHandlers.Admins;
public class UpdateCurrencyTypeCommandHandler : IRequestHandler<UpdateCurrencyTypeCommand, int>
{
    private readonly ApplicationDbContext _context;

    public UpdateCurrencyTypeCommandHandler(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<int> Handle(UpdateCurrencyTypeCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new InvalidNullInputException(nameof(request));
        var entity = _context.Set<CurrencyType>();

        var existingCurrency = await entity.FindAsync(request.Id, cancellationToken);
        if (existingCurrency is null)
            throw new NotFoundException("Wallet type not found");

        existingCurrency.Currency = request.Currency;
        existingCurrency.CurrencyCode = request.CurrencyCode;
        if(!string.IsNullOrEmpty(existingCurrency.CurrencyLogo))
            existingCurrency.CurrencyLogo = request.CurrencyLogo;
        existingCurrency.DateModified = DateTime.Now;
        entity.Update(existingCurrency);
        int result = await _context.SaveChangesAsync();

        if (result < 1)
            throw new InsertUpdateException(nameof(CurrencyType), "Update");
        return result;
    }
}
