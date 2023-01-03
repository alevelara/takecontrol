﻿using takecontrol.API.IntegrationTests.Primitives;

namespace takecontrol.API.IntegrationTests;

[CollectionDefinition(SharedTestCollection.Name)]
public class SharedTestCollection : ICollectionFixture<CustomWebApplicationFactory<Program>>
{
    public const string Name = "IntegrationTests";
}