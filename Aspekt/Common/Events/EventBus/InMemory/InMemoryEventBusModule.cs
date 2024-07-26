namespace Aspekt.Common.Events.EventBus.InMemory;

using System.Reflection;

public static class InMemoryEventBusModule
{
    public static IServiceCollection AddInMemoryEventBus(this IServiceCollection services, Assembly assembly)
    {
        services.AddScoped<IEventBus, InMemoryEventBus>();
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

        return services;
    }
}
