using FluentValidation;

namespace takecontrol.Application.Features.Accounts.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(r => r.Email)
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("Esto no es un email válido")
            .NotEmpty().WithMessage("El email debe estar relleno")
            .NotNull().WithMessage("El email debe estar relleno");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("La contraseña no puede ser vacía")
            .NotNull().WithMessage("La contraseña no puede ser vacía");
    }
}
