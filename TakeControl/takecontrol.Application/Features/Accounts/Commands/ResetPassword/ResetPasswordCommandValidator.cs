using FluentValidation;
using Takecontrol.Domain.Utils;

namespace Takecontrol.Application.Features.Accounts.Commands.ResetPassword;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(c => c.Email)
           .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("Invalid email.")
           .NotEmpty().WithMessage("User email can not be empty")
           .NotNull().WithMessage("User email can not be null");

        RuleFor(c => c.NewPassword)
            .NotEmpty()
            .WithMessage("New password can not be empty")
            .NotNull()
            .WithMessage("New password can not be null")
            .MaximumLength(14)
            .WithMessage("New password size can not be higher than 14 characters")
            .MinimumLength(8)
            .WithMessage("New password size can not be smaller than 8 characters")
            .Must(x => ValitatorsUtil.HasAnyLowerCase(x))
            .WithMessage("New password must have any character in lowercase")
            .Must(x => ValitatorsUtil.HasAnyUpperCase(x))
            .WithMessage("New password must have any character in uppercase")
            .Must(x => ValitatorsUtil.HasAnyDigit(x))
            .WithMessage("New password must have any digit from 0 to 9")
            .Must(x => ValitatorsUtil.HasAnySymbol(x))
            .WithMessage("New password must have any symbol");

        RuleFor(c => c.CurrentPassword)
            .NotNull()
            .WithMessage("Current Password can not be null")
            .NotEmpty()
            .WithMessage("Current password can not be empty");
    }
}
