﻿using Takecontrol.Emails.Domain.Models.Templates.Enum;
using Takecontrol.Emails.Infrastructure.Repositories.Templates;
using Takecontrol.Shared.Tests.Constants;
using Takecontrol.Shared.Tests.MockContexts;
using Xunit;

namespace Takecontrol.Emails.Infrastructure.Tests.Repositories.Templates;

[Collection(SharedTestCollection.Name)]
[Trait("Category", Category.EmailIntegrationTests)]
public class TemplateReadRepositoryXUnitTests : IAsyncLifetime
{
    private readonly TakeControlEmailDb _dbContext;

    public TemplateReadRepositoryXUnitTests()
    {
        _dbContext = new TakeControlEmailDb();
    }

    [Fact]
    public async Task GetTemplateByTemplateType_Should_ReturnWelcomeTemplate_WhenTemplateAlreadyExists()
    {
        //Arrange
        await SeedData();

        var templateRepository = new TemplateReadRepository(_dbContext.Context);
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
        await _dbContext.ResetState();

        //Arrange
        var templateRepository = new TemplateReadRepository(_dbContext.Context);
        var welcomeTemplate = TemplateType.WELCOME;

        //Act
        var result = await templateRepository.GetTemplateByTemplateType(welcomeTemplate);

        //Assert
        Assert.Null(result);
    }

    private async Task SeedData()
    {
        await _dbContext.SeedData();
    }

    public Task InitializeAsync()
    {
        _dbContext.EnsureDatabase();
        return Task.CompletedTask;
    }

    public async Task DisposeAsync() => await _dbContext.ResetState();
}
