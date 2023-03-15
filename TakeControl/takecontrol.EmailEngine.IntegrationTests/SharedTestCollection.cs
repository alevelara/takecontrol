namespace Takecontrol.EmailEngine.IntegrationTests;

[CollectionDefinition(SharedTestCollection.Name)]
public class SharedTestCollection : ICollectionFixture<EmailDbContextFixture>
{
    public const string Name = "EmailIntegrationTests";
}
