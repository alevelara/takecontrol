using Takecontrol.API.IntegrationTests.Shared.MockContexts;

namespace Takecontrol.Infrastructure.IntegrationTests;

[CollectionDefinition(SharedTestCollection.Name)]
public class SharedTestCollection : ICollectionFixture<TakeControlDb>
{
    public const string Name = "InfrastructureIntegrationTests";
}
