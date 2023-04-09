using MediatR;
using WalletSolution.Application.Admins.Command;
using WalletSolution.Common.Exceptions;
using WalletSolution.Domain.Entities.WalletUsers;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.CommandHandlers.Admins;
public class UpdateUserStatusCommandHandler : IRequestHandler<UpdateUserStatusCommand, int>
{
    private readonly ApplicationDbContext _context;

    public UpdateUserStatusCommandHandler(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<int> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new InvalidNullInputException(nameof(request));
        var entity = _context.Set<WalletUser>();
        var existingUser = await entity.FindAsync(request.UserId, cancellationToken);
        if (existingUser is null)
            throw new NotFoundException("User not found");

        existingUser.Status = request.UserStatus;
        existingUser.DateModified = DateTime.Now;
        entity.Update(existingUser);
        int result = await _context.SaveChangesAsync();

        if (result < 1)
            throw new InsertUpdateException(nameof(WalletUser), "Update");
        return result;
    }
}
