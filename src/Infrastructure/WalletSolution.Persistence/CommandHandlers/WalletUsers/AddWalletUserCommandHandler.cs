using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using WalletSolution.Application.WalletUsers.Command;
using WalletSolution.Common.Exceptions;
using WalletSolution.Common.Utilities;
using WalletSolution.Domain.Entities;
using WalletSolution.Domain.Entities.Admins;
using WalletSolution.Domain.Entities.WalletUsers;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence.CommandHandlers.WalletUsers;
public class AddWalletUserCommandHandler : IRequestHandler<AddUserCommand, int>
{
    private readonly ApplicationDbContext _context;

    public AddWalletUserCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        if(request is null)
            throw new InvalidNullInputException(nameof(request));
        var existingUser = await _context.Set<WalletUser>().SingleOrDefaultAsync(x => x.Email  == request.Email);
        if (existingUser is not null)
            throw new ExistingRecordException("A user with the supllied Email already exists");
        var walletUserRole = await _context.Set<Role>().SingleOrDefaultAsync(x => x.RoleName == "WalletUser");
        var currencies = _context.Set<CurrencyType>().AsQueryable();
        var user = new WalletUser
        {
            Id = Guid.NewGuid(),
            Email = request.Email,            
            FirstName = request.FirstName,
            LastName = request.LastName,
            ProfilePicture = request.ProfilePicture,
            Wallets = new List<Wallet>
            {
                new Wallet
                {
                    CurrencyTypeId = currencies.FirstOrDefault(x => x.CurrencyCode == "NGN").Id,
                    Currency = "NGN",
                    Balance = 0
                },
                new Wallet
                {
                     CurrencyTypeId = currencies.FirstOrDefault(x => x.CurrencyCode == "USD").Id,
                     Currency = "USD",
                    Balance = 0
                },
                new Wallet
                {
                     CurrencyTypeId = currencies.FirstOrDefault(x => x.CurrencyCode == "GBP").Id,
                     Currency = "GBP",
                    Balance = 0
                }
            }
        };
        _context.Set<WalletUser>().Add(user);

        var appUser = new AppUser
        {
            Email = request.Email,
            Password = SecurityHelper.GetSha256Hash(request.Password),
            Roles = new List<UserRole> { new UserRole { RoleId = walletUserRole.Id } }
        };

        _context.Set<AppUser>().Add(appUser);

        int result =  await _context.SaveChangesAsync();
        if(result < 1)
            throw new InsertUpdateException("Sign-up", "Insert");
        return result;
    }
}
