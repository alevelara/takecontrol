using Microsoft.EntityFrameworkCore;
using Takecontrol.EmailEngine.IntegrationTests;
using Takecontrol.EmailEngine.Persistence.Contexts;
using Takecontrol.EmailEngine.Repositories.Emails;
using Takecontrol.EmailEngine.UnitTests.TestsData;

namespace Takecontrol.EmailEngine.UnitTests.Repositories.Emails;

[Collection(SharedTestCollection.Name)]
[Trait("Category", "EmailIntegrationTests")]
public class EmailWriteRepositoryXUnitTests : IAsyncLifetime
{
    private readonly EmailDbContextFixture _fixture;
    private readonly EmailDbContext _dbContext;

    public EmailWriteRepositoryXUnitTests(EmailDbContextFixture fixture)
    {
        _fixture = fixture;
        _dbContext = fixture.EmailDbContext;
    }

    [Fact]
    public async Task AddEmail_Should_ReturnEmail_WhenEntityIsInsertedInDB()
    {
        var repository = new EmailWriteRepository(_dbContext);
        var email = EmailTestData.CreateEmailForTest();

        var result = await repository.AddEmail(email);

        Assert.NotNull(result);
        Assert.Equal(email, result);
    }

    public async Task DisposeAsync()
    {
        if (await _dbContext.Database.CanConnectAsync())
        {
            await _dbContext.Emails.ExecuteDeleteAsync();
        }
    }

    public Task InitializeAsync() => Task.CompletedTask;
}
