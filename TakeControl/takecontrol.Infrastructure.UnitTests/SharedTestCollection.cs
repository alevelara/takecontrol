using takecontrol.API.IntegrationTests.Shared.MockContexts;

namespace takecontrol.Infrastructure.IntegrationTests;

[CollectionDefinition(SharedTestCollection.Name)]
public class SharedTestCollection : ICollectionFixture<TakeControlDb>
{
    public const string Name = "InfrastructureIntegrationTests";
}
