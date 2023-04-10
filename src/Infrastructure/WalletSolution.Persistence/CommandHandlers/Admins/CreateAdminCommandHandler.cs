using MediatR;
using Microsoft.EntityFrameworkCore;
using WalletSolution.Application.Admins.Command;
using WalletSolution.Common.Exceptions;
using WalletSolution.Common.Utilities;
using WalletSolution.Domain.Entities;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.CommandHandlers.Admins;
public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, int>
{
    private readonly ApplicationDbContext _context;

    public CreateAdminCommandHandler(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<int> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new InvalidNullInputException(nameof(request));
        var entity = _context.Set<AppUser>();
        var existingUser = await entity.FirstOrDefaultAsync(x => x.Email == request.Email);
        if (existingUser is not null)
            throw new ExistingRecordException("User already exists");
        var appUser = new AppUser
        {
            Email = request.Email,
            Password = SecurityHelper.GetSha256Hash(request.Password)
        };
        entity.Add(appUser);
        int result = await _context.SaveChangesAsync();
        if (result < 1)
            throw new InsertUpdateException(nameof(AppUser), "Insert");
        return result;
    }
}
