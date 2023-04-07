using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using WalletSolution.Application.WalletUsers.Command;
using WalletSolution.Common.Exceptions;
using WalletSolution.Domain.Entities.Admins;
using WalletSolution.Domain.Entities.WalletUsers;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.CommandHandlers.WalletUsers;
public class AddWalletUserCommandHandler : IRequestHandler<AddUserCommand, int>
{
    private readonly ApplicationDbContext _context;

    public AddWalletUserCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        if(request is null)
            throw new InvalidNullInputException(nameof(request));
        var existingUser = await _context.Set<WalletUser>().FirstOrDefaultAsync(x => x.Email  == request.Email);
        if (existingUser is not null)
            throw new ExistingRecordException("A user with the supllied Email already exists");
        var currencies = _context.Set<CurrencyType>().AsQueryable();
        var user = new WalletUser
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Wallets = new List<Wallet>
            {
                new Wallet
                {
                    CurrencyTypeId = currencies.AsQueryable().FirstOrDefault(x => x.CurrencyCode == "NGN").Id,
                    Balance = 0
                },
                new Wallet
                {
                     CurrencyTypeId = currencies.AsQueryable().FirstOrDefault(x => x.CurrencyCode == "USD").Id,
                    Balance = 0
                },
                new Wallet
                {
                     CurrencyTypeId = currencies.AsQueryable().FirstOrDefault(x => x.CurrencyCode == "GBP").Id,
                    Balance = 0
                }
            }
        };
        _context.Set<WalletUser>().Add(user);
        int result =  await _context.SaveChangesAsync();
        if(result < 1)
            throw new InsertUpdateException("", "Insert");
        return result;
    }
}
