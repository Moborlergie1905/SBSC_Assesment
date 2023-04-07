using MediatR;
using WalletSolution.Application.WalletUsers.Command;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.CommandHandlers.WalletUsers;
public class UpdateWalletBalanceCommandHandler : IRequestHandler<UpdateWalletBalanceCommand, int>
{
    private readonly ApplicationDbContext _context;

    public UpdateWalletBalanceCommandHandler(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }    

    public Task<int> Handle(UpdateWalletBalanceCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
