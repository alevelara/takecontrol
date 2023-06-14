namespace Takecontrol.Shared.Tests.Contracts;

public interface IDbConfiguration
{
    void EnsureDatabase();

    Task ResetState();

    Task SeedData();
}
