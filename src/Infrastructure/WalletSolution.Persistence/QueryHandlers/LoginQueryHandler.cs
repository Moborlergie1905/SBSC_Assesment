using MediatR;
using Microsoft.EntityFrameworkCore;
using WalletSolution.Application.WalletUsers.Query;
using WalletSolution.Application.WalletUsers.Query.QueryModels;
using WalletSolution.Common.Exceptions;
using WalletSolution.Common.Utilities;
using WalletSolution.Domain.Entities;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.QueryHandlers;
public class LoginQueryHandler : IRequestHandler<LoginQuery, UserModel>
{
    private readonly ApplicationDbContext _context;

    public LoginQueryHandler(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<UserModel> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new InvalidNullInputException(nameof(request));

        var userQry = await _context.Set<AppUser>().AsQueryable()
            .Include(x => x.Roles)
             .ThenInclude(x => x.Role)
            .Where(x => x.Email == request.Email && x.Password == SecurityHelper.GetSha256Hash(request.Password))
            .Select( x => new UserModel {
                Email = x.Email, 
                Roles = x.Roles
                .Select(y => y.Role.RoleName).ToList()}
            ).FirstOrDefaultAsync();
        if (userQry is null)
            throw new FailedAuthenticationException("Authentication failed");
        return userQry;
    }
}
