using RazorEngineCore;
using Takecontrol.Emails.Application.Contracts.Templates;

namespace Takecontrol.Emails.Infrastructure.Repositories.Services;

public class TemplateLoader : ITemplateLoader
{
    public string LoadTemplate(string template)
    {
        IRazorEngine razorEngine = new RazorEngine();
        IRazorEngineCompiledTemplate modifiedMailTemplate = razorEngine.Compile(template);

        return modifiedMailTemplate.Run();
    }
}
