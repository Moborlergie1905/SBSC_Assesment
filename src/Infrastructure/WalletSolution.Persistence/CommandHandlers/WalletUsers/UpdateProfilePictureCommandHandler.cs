using MediatR;
using Microsoft.EntityFrameworkCore;
using WalletSolution.Application.WalletUsers.Command;
using WalletSolution.Common.Exceptions;
using WalletSolution.Domain.Entities.WalletUsers;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.CommandHandlers.WalletUsers;
public class UpdateProfilePictureCommandHandler : IRequestHandler<UploadProfilePictureCommand, int>
{
    private readonly ApplicationDbContext _context;

    public UpdateProfilePictureCommandHandler(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<int> Handle(UploadProfilePictureCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new InvalidNullInputException(nameof(request));
        var entity = _context.Set<WalletUser>();
        var existingUser = await entity.FirstOrDefaultAsync(x => x.Email == request.Email);
        if (existingUser is null)
            throw new NotFoundException("Not an existing user");
        existingUser.ProfilePicture = request.ProfilePicture;
        existingUser.DateModified = DateTime.Now;
        entity.Update(existingUser);
        int result = await _context.SaveChangesAsync();
        if (result < 1)
            throw new InsertUpdateException("WalletUser", "Update");
        return result;
    }
}
