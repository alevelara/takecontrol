namespace takecontrol.API.IntegrationTests.Contracts;

public interface IDbConfiguration
{
    Task EnsureDatabase();

    Task ResetState();

    Task SeedData();
}
