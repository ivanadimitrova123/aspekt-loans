namespace Aspekt.IntegrationTests.Common.Events.EventBus.InMemory;

using Aspekt.Common.Events;

internal sealed class TestEventConsumer : IIntegrationEventHandler<FakeEvent>
{
    public Task Handle(FakeEvent @event, CancellationToken cancellationToken)
    {
        @event.MarkAsConsumed();
        return Task.CompletedTask;
    }
}
