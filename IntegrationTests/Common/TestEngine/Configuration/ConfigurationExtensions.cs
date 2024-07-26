namespace Aspekt.IntegrationTests.Common.TestEngine.Configuration;

using System.Reflection;
using Aspekt.Common.Events.EventBus;
using Aspekt.Common.Events.EventBus.InMemory;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

internal static class ConfigurationExtensions
{
    internal static WebApplicationFactory<T> WithContainerDatabaseConfigured<T>(this WebApplicationFactory<T> webApplicationFactory, string connectionString)
        where T : class
    {
        var connectionStringsConfiguration = new Dictionary<string, string?>
        {
            {ConfigurationKeys.ContactsConnectionString, connectionString},
            {ConfigurationKeys.ApplicationsConnectionString, connectionString},
            {ConfigurationKeys.LoansConnectionString, connectionString},
        };

        return webApplicationFactory.UseSettings(connectionStringsConfiguration);
    }

    private static WebApplicationFactory<T> UseSettings<T>(this WebApplicationFactory<T> webApplicationFactory, Dictionary<string, string?> settings)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder =>
        {
            foreach (var setting in settings)
            {
                webHostBuilder.UseSetting(setting.Key, setting.Value);
            }
        });

    internal static WebApplicationFactory<T> WithFakeEventBus<T>(this WebApplicationFactory<T> webApplicationFactory, IEventBus eventBusMock)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder => webHostBuilder.ConfigureTestServices(services =>
            services.AddSingleton(eventBusMock)));

    internal static WebApplicationFactory<T> WithFakeConsumers<T>(this WebApplicationFactory<T> webApplicationFactory)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder => webHostBuilder.ConfigureTestServices(services =>
            services.AddInMemoryEventBus(Assembly.GetExecutingAssembly())));
}
