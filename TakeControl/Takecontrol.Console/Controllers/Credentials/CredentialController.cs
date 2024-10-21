using Spectre.Console;
using Takecontrol.Console.Domain.Credentials;
using Takecontrol.Console.Services.Credentials;

namespace Takecontrol.Console.Controllers.Credentials;

public sealed class CredentialController
{
    private readonly ICredentialService _credentialService;
    private static Dictionary<Guid, Claim> _claims;

    public CredentialController(ICredentialService credentialService)
    {
        _credentialService = credentialService;
        _claims = new Dictionary<Guid, Claim>();
    }

    public async Task Login()
    {
        var user = AnsiConsole.Prompt(
            new TextPrompt<string>("Indique su nombre de usuario: "));
        AnsiConsole.MarkupLineInterpolated($"Bienvenido [green]{user}[/]");

        var password = AnsiConsole.Prompt(
            new TextPrompt<string>("Indique su contraseña: ").Secret());

        var result = await _credentialService.Login(user, password);
        if (result == null)
        {
            AnsiConsole.MarkupLineInterpolated($"[red]usuario {user} invalido o contraseña incorrecta.[/]");
            await Login();
        }
        else
        {
            if (!_claims.ContainsKey(result.Id))
            {
                _claims.Add(result.Id, new Claim(result.UserType, result.Token));
            }

            AnsiConsole.MarkupLineInterpolated($"[green]{user} ha sido logeado con éxito.[/]");
            await UpdatePassword(result.Token);
        }
    }

    public async Task ResetPassword()
    {
        AnsiConsole.MarkupLineInterpolated($"Para resetear su contraseña, por favor, indique los siguientes parametros:");
        var user = AnsiConsole.Prompt(
            new TextPrompt<string>("Indique su nombre de usuario: "));
        AnsiConsole.MarkupLineInterpolated($"Bienvenido [green]{user}[/]");

        var currentPassword = AnsiConsole.Prompt(
            new TextPrompt<string>("Indique su contraseña actual: ").Secret());

        var newPassword = AnsiConsole.Prompt(
            new TextPrompt<string>("Indique su nueva contraseña: ").Secret());

        var result = await _credentialService.ResetPassword(user, currentPassword, newPassword);

        if (!result)
        {
            AnsiConsole.MarkupLineInterpolated($"[red]Un error ocurrió mientras se intentaba resetear la constraseña.[/]");
            await ResetPassword();
        }
        else
        {
            AnsiConsole.MarkupLineInterpolated($"[green]La contraseña ha sido reseteada con exito.[/]");
        }
    }

    public async Task UpdatePassword(string token)
    {
        AnsiConsole.MarkupLineInterpolated($"Para actualizar su contraseña, por favor, indique los siguientes parametros:");

        var user = AnsiConsole.Prompt(
            new TextPrompt<string>("Indique su nombre de usuario: "));
        AnsiConsole.MarkupLineInterpolated($"Bienvenido [green]{user}[/]");

        var password = AnsiConsole.Prompt(
            new TextPrompt<string>("Indique su nueva contraseña: ").Secret());

        var result = await _credentialService.UpdatePassword(user, password, token);

        if (!result)
        {
            AnsiConsole.MarkupLineInterpolated($"[red]Un error ocurrió mientras se intentaba actualizar la constraseña.[/]");
            await UpdatePassword(token);
        }
        else
        {
            AnsiConsole.MarkupLineInterpolated($"[green]La contraseña ha sido actualizada con exito.[/]");
        }
    }
}
