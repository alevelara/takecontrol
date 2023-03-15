using Takecontrol.Domain.Models.Templates.Enum;
using Takecontrol.Domain.Models.Templates.ValueObjects;
using Takecontrol.Domain.Primitives;

namespace Takecontrol.Domain.Models.Templates
{
    public class Template : BaseDomainModel
    {
        public Guid Id { get; private set; }

        public TemplateType TemplateType { get; private set; }

        public virtual string Payload { get; private set; }

        public string Language { get; private set; }

        public Template(TemplateType templateType, string payload, string language)
        {
            Id = new TemplateId().Value;
            TemplateType = templateType;
            Payload = payload;
            Language = language;
        }

        public static Template Create(TemplateType templateType, string payload, string language)
        {
            return new Template(templateType, payload, language);
        }
    }
}
