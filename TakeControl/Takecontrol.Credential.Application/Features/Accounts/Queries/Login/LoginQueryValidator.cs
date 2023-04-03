using FluentValidation;

namespace Takecontrol.Credential.Application.Features.Accounts.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(r => r.Email)
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("Invalid email.")
            .NotEmpty().WithMessage("User email can not be empty")
            .NotNull().WithMessage("User email can not be null");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("Password can not be empty")
            .NotNull().WithMessage("Password can no be null");
    }
}
