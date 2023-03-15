using RazorEngineCore;
using Takecontrol.Application.Contracts.Templates;

namespace Takecontrol.EmailEngine.Services
{
    public class TemplateLoader : ITemplateLoader
    {
        public string LoadTemplate(string template)
        {
            IRazorEngine razorEngine = new RazorEngine();
            IRazorEngineCompiledTemplate modifiedMailTemplate = razorEngine.Compile(template);

            return modifiedMailTemplate.Run();
        }
    }
}
