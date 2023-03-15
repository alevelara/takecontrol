namespace Takecontrol.IntegrationTests.Shared.Contracts;

public interface IDbConfiguration
{
    Task EnsureDatabase();

    Task ResetState();

    Task SeedData();
}
