using Xunit;

namespace Takecontrol.Emails.Infrastructure.Tests;

[CollectionDefinition(Name)]
public class SharedTestCollection : ICollectionFixture<EmailDbContextFixture>
{
    public const string Name = "EmailIntegrationTests";
}
