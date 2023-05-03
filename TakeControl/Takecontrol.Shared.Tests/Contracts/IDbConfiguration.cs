namespace Takecontrol.Shared.Tests.Contracts;

public interface IDbConfiguration
{
    Task EnsureDatabase();

    Task ResetState();

    Task SeedData();
}
