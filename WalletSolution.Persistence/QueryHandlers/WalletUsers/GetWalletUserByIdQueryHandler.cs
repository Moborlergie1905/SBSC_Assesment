using MediatR;
using WalletSolution.Application.WalletUsers.Query;
using WalletSolution.Application.WalletUsers.Query.QueryModels;
using WalletSolution.Common.Exceptions;
using WalletSolution.Domain.Entities.WalletUsers;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.QueryHandlers.WalletUsers;
public class GetWalletUserByIdQueryHandler : IRequestHandler<GetWalletUserByIdQuery, WalletUserQueryModel>
{
    private readonly ApplicationDbContext _context;

    public GetWalletUserByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));        
    }

    public async Task<WalletUserQueryModel> Handle(GetWalletUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Set<WalletUser>().FindAsync(request.Id);
        if (user is null)
            throw new NotFoundException(nameof(WalletUser), request.Id);
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
