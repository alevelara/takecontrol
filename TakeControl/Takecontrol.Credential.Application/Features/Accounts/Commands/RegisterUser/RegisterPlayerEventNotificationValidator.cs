using FluentValidation;
using Takecontrol.Shared.Application.Events.Credentials;
using Takecontrol.Shared.Domain.Utils;

namespace Takecontrol.Credential.Application.Features.Accounts.Commands.RegisterUser;

public class RegisterPlayerEventNotificationValidator : AbstractValidator<RegisterPlayerEventNotification>
{
    public RegisterPlayerEventNotificationValidator()
    {
        RuleFor(c => c.Email)
          .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("Invalid email.")
          .NotEmpty().WithMessage("User email can not be empty")
          .NotNull().WithMessage("User email can not be null");

        RuleFor(c => c.Name)
          .NotEmpty().WithMessage("User name can not be empty")
          .NotNull().WithMessage("User namne can not be null");

        RuleFor(c => c.Password)
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
    }
}
