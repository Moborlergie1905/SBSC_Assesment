using FluentValidation;
using WalletSolution.API.Controllers.WalletUsers.Requests;

namespace WalletSolution.API.Controllers.WalletUsers.Validator;
public class AddWalletUserValidator : AbstractValidator<AddWalletUserRequest>
{
    public AddWalletUserValidator()
    {
        RuleFor(x => x.FirstName)
            .NotNull()
            .NotEmpty()
            .WithMessage("{PropertyName} is not valid");
        RuleFor(x => x.LastName)
           .NotNull()
           .NotEmpty()
           .WithMessage("{PropertyName} is not valid");
        RuleFor(x => x.Email)
           .EmailAddress()
           .NotNull()
           .NotEmpty()
           .WithMessage("{PropertyName} is not valid");
        RuleFor(x => x.Password)
           .NotNull()
           .NotEmpty()
           .WithMessage("{PropertyName} is not valid");
    }
}
