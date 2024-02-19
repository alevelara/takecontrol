namespace Takecontrol.Emails.Application.Contracts.Templates;

public interface ITemplateLoader
{
    Task<string> LoadTemplate(string template, object? model);
}
