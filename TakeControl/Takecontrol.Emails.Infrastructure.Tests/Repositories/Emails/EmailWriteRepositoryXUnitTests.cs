using Microsoft.EntityFrameworkCore;
using Takecontrol.Emails.Infrastructure.Contexts;
using Takecontrol.Emails.Infrastructure.Repositories.Emails;
using Takecontrol.Emails.Infrastructure.Tests.TestsData;
using Takecontrol.Shared.Tests.MockContexts;
using Xunit;

namespace Takecontrol.Emails.Infrastructure.Tests.Repositories.Emails;

[Collection(SharedTestCollection.Name)]
[Trait("Category", "EmailIntegrationTests")]
public class EmailWriteRepositoryXUnitTests : IAsyncLifetime
{
    private readonly TakeControlEmailDb _dbContext;

    public EmailWriteRepositoryXUnitTests()
    {
        _dbContext = new TakeControlEmailDb();
    }

    [Fact]
    public async Task AddEmail_Should_ReturnEmail_WhenEntityIsInsertedInDB()
    {
        var repository = new EmailWriteRepository(_dbContext.Context);
        var email = EmailTestData.CreateEmailForTest();

        var result = await repository.AddEmail(email);

        Assert.NotNull(result);
        Assert.Equal(email, result);
    }

    public async Task DisposeAsync()
    {
        await _dbContext.ResetState();
    }

    public Task InitializeAsync() => Task.CompletedTask;
}
