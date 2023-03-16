using Microsoft.EntityFrameworkCore;
using Takecontrol.Emails.Infrastructure.Contexts;
using Takecontrol.Emails.Infrastructure.Repositories.Emails;
using Takecontrol.Emails.Infrastructure.Tests.TestsData;
using Xunit;

namespace Takecontrol.Emails.Infrastructure.Tests.Repositories.Emails;

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
