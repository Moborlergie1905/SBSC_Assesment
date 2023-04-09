using MediatR;
using Microsoft.EntityFrameworkCore;
using WalletSolution.Application.WalletUsers.Query;
using WalletSolution.Application.WalletUsers.Query.QueryModels;
using WalletSolution.Common.Exceptions;
using WalletSolution.Domain.Entities.WalletUsers;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.QueryHandlers.WalletUsers;
public class GetWalletUserByEmailQueryHandler : IRequestHandler<GetWalletUserByEmailQuery, WalletUserQueryModel>
{
    private readonly ApplicationDbContext _context;

    public GetWalletUserByEmailQueryHandler(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<WalletUserQueryModel> Handle(GetWalletUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Set<WalletUser>().FirstOrDefaultAsync(x => x.Email == request.Email);
        if (user is null)
            throw new NotFoundException(nameof(WalletUser), request.Email);
        var userModel = new WalletUserQueryModel
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            ProfilePicture = user.ProfilePicture
        };
        return userModel;
    }
}
