using FluentValidation;
using Takecontrol.Shared.Domain.Utils;

namespace Takecontrol.User.Application.Features.Clubs.Commands.RegisterClub;

public class RegisterClubCommandValidator : AbstractValidator<RegisterClubCommand>
{
    public RegisterClubCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Club Name can not be empty")
            .NotNull()
            .WithMessage("Club Name can not be null");

        RuleFor(c => c.City)
            .NotEmpty()
            .WithMessage("Club city can not be empty")
            .NotNull()
            .WithMessage("Club city can not be null");

        RuleFor(c => c.MainAddress)
            .NotEmpty()
            .WithMessage("Club address can not be empty")
            .NotNull()
            .WithMessage("Club address can not be null");

        RuleFor(c => c.Province)
            .NotEmpty()
            .WithMessage("Club province can not be empty")
            .NotNull()
            .WithMessage("Club province can not be null");

        RuleFor(c => c.Email)
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("Invalid email.")
            .NotEmpty().WithMessage("User email can not be empty")
            .NotNull().WithMessage("User email can not be null");

        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage("Password can not be empty")
            .NotNull()
            .WithMessage("Password can not be null")
            .MaximumLength(14)
            .WithMessage("Password size can not be higher than 14 characters")
            .MinimumLength(8)
            .WithMessage("Password size can not be smaller than 8 characters")
            .Must(x => ValitatorsUtil.HasAnyLowerCase(x))
            .WithMessage("Password must have any character in lowercase")
            .Must(x => ValitatorsUtil.HasAnyUpperCase(x))
            .WithMessage("Password must have any character in uppercase")
            .Must(x => ValitatorsUtil.HasAnyDigit(x))
            .WithMessage("Password must have any digit from 0 to 9")
            .Must(x => ValitatorsUtil.HasAnySymbol(x))
            .WithMessage("Password must have any symbol");

        RuleFor(c => c.NumberOfCourts)
            .GreaterThan(0)
            .WithMessage("Club must have at least one court.");

        RuleFor(c => c.ClosureDate)
            .GreaterThan(c => c.OpenDate.AddHours(1).AddMinutes(30))
            .WithMessage("End Date can not be smaller than init date plus 1 hour and 30 mins");
    }
}
