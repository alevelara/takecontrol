namespace takecontrol.API.IntegrationTests.Contracts;

public interface IDbConfiguration
{
    void EnsureDatabase();

    Task ResetState();
}
