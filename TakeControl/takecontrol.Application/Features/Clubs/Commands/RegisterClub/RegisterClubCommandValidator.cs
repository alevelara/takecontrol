using FluentValidation;
using System.Text.RegularExpressions;
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
            .Must(x => HasAnyLowerCase(x))
            .WithMessage("Password must have any character in lowercase")
            .Must(x => HasAnyUpperCase(x))
            .WithMessage("Password must have any character in uppercase")
            .Must(x => HasAnyDigit(x))
            .WithMessage("Password must have any digit from 0 to 9")
            .Must(x => HasAnySymbol(x))
            .WithMessage("Password must have any symbol");
    }

    private bool HasAnyLowerCase(string pw)
    {
        var lowercase = new Regex("[a-z]+");
        bool result = false;
        if (!string.IsNullOrEmpty(pw))
        {
            result = lowercase.IsMatch(pw);
        }

        return result;
    }

    private bool HasAnyUpperCase(string pw)
    {
        var uppercase = new Regex("[A-Z]+");
        bool result = false;
        if (!string.IsNullOrEmpty(pw))
        {
            result = uppercase.IsMatch(pw);
        }
        return result;
    }

    private bool HasAnyDigit(string pw)
    {
        var digit = new Regex("(\\d)+");
        bool result = false;
        if (!string.IsNullOrEmpty(pw))
        {
            result = digit.IsMatch(pw);
        }
        return result;
    }

    private bool HasAnySymbol(string pw)
    {
        var symbol = new Regex("(\\W)+");
        bool result = false;
        if (!string.IsNullOrEmpty(pw))
        {
            result = symbol.IsMatch(pw);
        }
        return result;
    }

}
