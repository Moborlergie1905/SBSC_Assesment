using MediatR;
using Microsoft.EntityFrameworkCore;
using WalletSolution.Application.Admins.Command;
using WalletSolution.Common.Exceptions;
using WalletSolution.Domain.Entities;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.CommandHandlers.Admins;
public class AssignAdminRoleCommandHandler : IRequestHandler<AssignAdminRoleCommand, int>
{
    private readonly ApplicationDbContext _context;

    public AssignAdminRoleCommandHandler(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<int> Handle(AssignAdminRoleCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new InvalidNullInputException(nameof(request));
        var entity = _context.Set<UserRole>();
        var existingRole = await entity
            .FirstOrDefaultAsync(x => x.AppUserId == request.UserId &&  x.RoleId == request.RoleId);
        if (existingRole is not null)
            throw new ExistingRecordException("The user already assigned to the selected role");
        var userRole = new UserRole
        {
            AppUserId = request.UserId,
            RoleId = request.RoleId
        };
        entity.Add(userRole);
        int result = await _context.SaveChangesAsync();
        if (result < 1)
            throw new InsertUpdateException(nameof(UserRole), "Insert");
        return result;
    }
}
