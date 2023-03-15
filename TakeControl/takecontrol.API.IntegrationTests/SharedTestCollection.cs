using Takecontrol.API.IntegrationTests.Primitives;
using Xunit;

namespace Takecontrol.API.IntegrationTests;

[CollectionDefinition(SharedTestCollection.Name)]
public class SharedTestCollection : ICollectionFixture<ApiWebApplicationFactory<Program>>
{
    public const string Name = "IntegrationTests";
}
