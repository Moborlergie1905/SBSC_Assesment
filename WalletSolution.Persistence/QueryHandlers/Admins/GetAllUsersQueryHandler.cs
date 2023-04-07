using MediatR;
using Microsoft.EntityFrameworkCore;
using WalletSolution.Application.WalletUsers.Query;
using WalletSolution.Application.WalletUsers.Query.QueryModels;
using WalletSolution.Common.Exceptions;
using WalletSolution.Domain.Entities.WalletUsers;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.QueryHandlers.Admins;
public class GetAllUsersQueryHandler : IRequestHandler<GetWalletUserListQuery, List<WalletUserQueryModel>>
{
    private readonly ApplicationDbContext _context;

    public GetAllUsersQueryHandler(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<WalletUserQueryModel>> Handle(GetWalletUserListQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new InvalidNullInputException(nameof(request));

        var entity = _context.Set<WalletUser>();
        var users = await entity.ToListAsync(cancellationToken);

        List<WalletUserQueryModel> walletUserQueryModels = new List<WalletUserQueryModel>();
        users.ForEach(user =>
        {
            walletUserQueryModels.Add(new WalletUserQueryModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Status = user.Status,
                DateCreated = user.DateCreated,
                DateModified = user.DateModified
            });
        });
        return walletUserQueryModels;
    }
}
