using Takecontrol.Domain.Models.Emails.Enums;
using Takecontrol.Domain.Models.Templates;
using Takecontrol.Domain.Models.Templates.Enum;

namespace Takecontrol.Domain.UnitTests.Models.Templates;

[Trait("Category", "UnitTests")]
public class TemplateXUnitTests
{
    [Fact]
    public void Create_Should_ReturnNewTemplate_WhenAllFieldsArePopulated()
    {
        var payload = "payloadTest";
        var language = "Language";
        var template = Template.Create(TemplateType.WELCOME, payload, language);

        Assert.NotNull(template);
        Assert.Equal(template.Payload, payload);
        Assert.Equal(template.Language, language);
        Assert.Equal(TemplateType.WELCOME, template.TemplateType);
    }

    [Fact]
    public void Create_Should_ReturnNewTemplate_WhenPayloadIsNull()
    {
        var payload = "payloadTest";
        var language = "Language";
        var template = Template.Create(TemplateType.WELCOME, null, language);

        Assert.NotNull(template);
        Assert.Null(template.Payload);
        Assert.Equal(template.Language, language);
        Assert.Equal(TemplateType.WELCOME, template.TemplateType);
    }

    [Fact]
    public void Create_Should_ReturnNewTemplate_WhenLanguageIsNUll()
    {
        var payload = "payloadTest";
        var template = Template.Create(TemplateType.WELCOME, payload, null);

        Assert.NotNull(template);
        Assert.Null(template.Language);
        Assert.Equal(template.Payload, payload);
        Assert.Equal(TemplateType.WELCOME, template.TemplateType);
    }
}
