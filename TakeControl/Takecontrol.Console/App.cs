using Spectre.Console;
using Takecontrol.Console.Controllers.Credentials;

namespace Takecontrol.Console;

public class App
{
    public readonly CredentialController _credentialController;
    private const string Login = "Login";
    private const string ResetPassword = "Resetea contraseña";
    private const string UpdatePassword = "Actualiza contraseña";

    public App(CredentialController credentialController)
    {
        _credentialController = credentialController;
    }

    public async Task RunAsync(string[] args)
    {
        var action = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
        .Title("Bienvenido, [green]empezamos[/]?")
        .PageSize(3)
        .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
        .AddChoices(new[]
        {
            Login, ResetPassword, UpdatePassword
        }));

        switch (action)
        {
            case Login:
                await _credentialController.Login();
                break;
            case ResetPassword:
                await _credentialController.ResetPassword();
                break;
            case UpdatePassword:
                await _credentialController.UpdatePassword(string.Empty);
                break;
            default:
                break;
        }

        AnsiConsole.Prompt(new TextPrompt<string>("ahora que.."));
    }
}
