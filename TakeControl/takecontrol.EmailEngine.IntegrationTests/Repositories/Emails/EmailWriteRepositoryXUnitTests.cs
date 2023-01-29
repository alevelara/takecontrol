using takecontrol.EmailEngine.IntegrationTests;
using takecontrol.EmailEngine.Persistence.Contexts;
using takecontrol.EmailEngine.Repositories.Emails;
using takecontrol.EmailEngine.UnitTests.TestsData;

namespace takecontrol.EmailEngine.UnitTests.Repositories.Emails;

[Collection(SharedTestCollection.Name)]
[Trait("Category", "EmailIntegrationTests")]

public class EmailWriteRepositoryXUnitTests : IAsyncLifetime
{
    private EmailDbContextFixture _fixture;
    private EmailDbContext _dbContext;

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
		Assert.NotNull(result.Id);
		Assert.Equal(email, result);
	}

    public async Task DisposeAsync()
    {
        await _fixture.DisposeAsync();
    }

    public async Task InitializeAsync()
    {
       await _fixture.InitializeAsync();
    }
}
