using MediatR;
using Microsoft.EntityFrameworkCore;
using WalletSolution.Application.WalletUsers.Query;
using WalletSolution.Application.WalletUsers.Query.QueryModels;
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
        var entity = _context.Set<WalletUser>();
        var users = await entity
            .Select(x => new WalletUserQueryModel
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Status = x.Status,
                DateCreated = x.DateCreated,
                DateModified = x.DateModified
            }).ToListAsync(cancellationToken);        
        return users;
    }
}
