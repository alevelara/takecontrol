namespace takecontrol.IntegrationTests.Shared.Contracts;

public interface IDbConfiguration
{
    Task EnsureDatabase();

    Task ResetState();

    Task SeedData();
}
