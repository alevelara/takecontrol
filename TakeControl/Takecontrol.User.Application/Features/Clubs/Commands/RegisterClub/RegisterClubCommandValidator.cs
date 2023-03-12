using System.Text.RegularExpressions;
using FluentValidation;
using takecontrol.Domain.Utils;
using Takecontrol.User.Application.Features.Clubs.Commands.RegisterClub;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace takecontrol.Application.Features.Clubs.Commands.RegisterClub;

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
    }
}
