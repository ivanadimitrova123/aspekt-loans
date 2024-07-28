namespace Aspekt.IntegrationTests.Common.TestEngine;

public sealed class DatabaseContainer : IAsyncLifetime
{
    internal string? ConnectionString;
    private IConfiguration? _configuration;

    public async Task InitializeAsync()
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        ConnectionString = _configuration.GetConnectionString("DefaultConnection");

        await Task.CompletedTask; 
    }

    public async Task DisposeAsync()
    {
        await Task.CompletedTask; 
    }

    public IConfiguration GetConfiguration() => _configuration!;
}
