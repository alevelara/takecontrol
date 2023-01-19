using takecontrol.Domain.Models.Templates.Enum;
using takecontrol.Domain.Primitives;

namespace takecontrol.Domain.Models.Templates
{
    public class Template : BaseDomainModel
    {
        public Guid Id { get; set; }

        public TemplateType TemplateType { get; set; }

        public string Payload { get; set; }

        public string Language { get; set; }

    }
}
