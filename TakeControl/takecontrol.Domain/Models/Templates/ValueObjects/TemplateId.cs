using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using takecontrol.Domain.Primitives;

namespace takecontrol.Domain.Models.Templates.ValueObjects
{
    public class TemplateId : ValueObject
    {
        public Guid Value { get; private set; }

        public TemplateId()
        {
            Value = Guid.NewGuid();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
