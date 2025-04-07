namespace StixApi.IntegrationTests.Fixtures;

public class IntegrationTest : IClassFixture<StixApiWebApplicationFactory>
{
    protected readonly StixApiWebApplicationFactory _factory;
    protected readonly HttpClient _client;

    public IntegrationTest(StixApiWebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }
}
