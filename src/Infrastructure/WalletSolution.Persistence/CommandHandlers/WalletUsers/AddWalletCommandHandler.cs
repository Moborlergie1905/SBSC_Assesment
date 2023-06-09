﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using WalletSolution.Application.WalletUsers.Command;
using WalletSolution.Common.Exceptions;
using WalletSolution.Domain.Entities.Admins;
using WalletSolution.Domain.Entities.WalletUsers;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.CommandHandlers.WalletUsers;
public class AddWalletCommandHandler : IRequestHandler<AddWalletCommand, int>
{
    private readonly ApplicationDbContext _context;

    public AddWalletCommandHandler(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<int> Handle(AddWalletCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new InvalidNullInputException(nameof(request));

        var user = await _context.Set<WalletUser>().FirstOrDefaultAsync(x => x.Email == request.Email);

        var existingCurrencyType = await _context.Set<CurrencyType>().FirstOrDefaultAsync(x => x.CurrencyCode == request.Currency);

        if (existingCurrencyType is null)
            throw new NotFoundException("Selected currenecy type is not found");

        var entity = _context.Set<Wallet>();
        var existingCurrency = entity
            .FirstOrDefaultAsync(x => x.WalletUserId == user.Id && x.CurrencyTypeId == existingCurrencyType.Id);
        if (existingCurrency is not null)
            throw new ExistingRecordException("Selected currency is already added to your wallet");
        var wallet = new Wallet
        {
            WalletUserId = user.Id,
            CurrencyTypeId = existingCurrencyType.Id,
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
