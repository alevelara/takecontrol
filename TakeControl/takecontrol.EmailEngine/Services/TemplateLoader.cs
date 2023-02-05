using RazorEngineCore;
using takecontrol.Application.Contracts.Templates;

namespace takecontrol.EmailEngine.Services
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
