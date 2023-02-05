using Microsoft.EntityFrameworkCore;
using takecontrol.Domain.Models.Templates;
using takecontrol.Domain.Models.Templates.Enum;
using takecontrol.EmailEngine.Persistence.Contexts;
using takecontrol.EmailEngine.Persistence.Data;
using takecontrol.EmailEngine.Repositories.Templates;

namespace takecontrol.EmailEngine.IntegrationTests.Repositories.Templates;

[Collection(SharedTestCollection.Name)]
[Trait("Category", "EmailIntegrationTests")]
public class TemplateReadRepositoryXUnitTests : IAsyncLifetime
{

    private EmailDbContextFixture _fixture;
    private EmailDbContext _dbContext;

    public TemplateReadRepositoryXUnitTests(EmailDbContextFixture fixture)
    {
        _fixture = fixture;
        _dbContext = _fixture.EmailDbContext;
    }

    [Fact]
    public async Task GetTemplateByTemplateType_Should_ReturnWelcomeTemplate_WhenTemplateAlreadyExists()
    {
        //Arrange
        await SeedData();

        var templateRepository = new TemplateReadRepository(_dbContext);
        var welcomeTemplate = TemplateType.WELCOME;
        //Act

        var result = await templateRepository.GetTemplateByTemplateType(welcomeTemplate);

        //Assert

        Assert.NotNull(result);
        Assert.Equal(TemplateType.WELCOME, result.TemplateType);
    }

    [Fact]
    public async Task GetTemplateByTemplateType_Should_ReturnNullValue_WhenTemplateDoesntExist()
    {
        //Arrange
        var templateRepository = new TemplateReadRepository(_dbContext);
        var welcomeTemplate = TemplateType.WELCOME;
        //Act

        var result = await templateRepository.GetTemplateByTemplateType(welcomeTemplate);

        //Assert

        Assert.Null(result);
    }

    private async Task SeedData()
    {
        await _dbContext.Set<Template>().AddAsync(TemplateFactory.WelcomeTemplate);
        await _dbContext.SaveChangesAsync();
    }

    public Task InitializeAsync() => Task.CompletedTask;


    public async Task DisposeAsync()
    {
        if (await _dbContext.Database.CanConnectAsync())
        {
            await _dbContext.Emails.ExecuteDeleteAsync();
            await _dbContext.Templates.ExecuteDeleteAsync();
        }
    }
}
