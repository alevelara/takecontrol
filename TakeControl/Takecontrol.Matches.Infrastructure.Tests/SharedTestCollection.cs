using Takecontrol.Shared.Tests.MockContexts;
using Xunit;

namespace Takecontrol.Matches.Infrastructure.Tests;

[CollectionDefinition(Name)]
public class SharedTestCollection : ICollectionFixture<TakeControlMatchesDb>
{
    public const string Name = "InfrastructureIntegrationTests";
}
