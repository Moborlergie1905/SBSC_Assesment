﻿using MediatR;
using WalletSolution.Application.WalletUsers.Command;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.CommandHandlers.WalletUsers;
public class UpdateWalletBalanceCommandHandler : IRequestHandler<FundWalletBalanceCommand, int>
{
    private readonly ApplicationDbContext _context;

    public UpdateWalletBalanceCommandHandler(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }    

    public Task<int> Handle(FundWalletBalanceCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}