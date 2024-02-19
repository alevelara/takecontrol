using RazorEngineCore;
using Takecontrol.Emails.Application.Contracts.Templates;

namespace Takecontrol.Emails.Infrastructure.Repositories.Services;

public class TemplateLoader : ITemplateLoader
{
    public async Task<string> LoadTemplate(string template, object? model)
    {
        IRazorEngine razorEngine = new RazorEngine();
        IRazorEngineCompiledTemplate modifiedMailTemplate = await razorEngine.CompileAsync(template);

        return await modifiedMailTemplate.RunAsync(model);
    }
}
