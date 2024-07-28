using Aspekt.Common.Events.EventBus;
using Aspekt.IntegrationTests.Common.TestEngine.Configuration;

namespace Aspekt.IntegrationTests.Common.Events.EventBus.InMemory;

public sealed class InMemoryEventBusTests(
    WebApplicationFactory<Program> applicationFactory,
    DatabaseContainer database) : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private readonly WebApplicationFactory<Program> _application = applicationFactory
        .WithDatabaseConfigured(database.GetConfiguration())
        .WithFakeConsumers();

    [Fact]
    public async Task Given_valid_event_published_Then_event_should_be_consumed()
    {
        // Arrange
        var eventBus = GetEventBus();
        var fakeEvent = FakeEvent.Create();

        // Act
        await eventBus!.PublishAsync(fakeEvent, CancellationToken.None);

        // Assert
        fakeEvent.Consumed.Should().BeTrue();
    }

    private IEventBus GetEventBus() =>
        _application.Services
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<IEventBus>();
}
