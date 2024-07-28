using Aspekt.Common.Events.EventBus;
using Aspekt.Common.Events.EventBus.InMemory;
using Microsoft.AspNetCore.TestHost;
using System.Reflection;

namespace Aspekt.IntegrationTests.Common.TestEngine.Configuration;

internal static class ConfigurationExtensions
{
    internal static WebApplicationFactory<T> WithDatabaseConfigured<T>(this WebApplicationFactory<T> webApplicationFactory, IConfiguration configuration)
        where T : class
    {
        var connectionStrings = new Dictionary<string, string?>
        {
            {ConfigurationKeys.ContactsConnectionString, configuration.GetConnectionString("Contacts")},
            {ConfigurationKeys.ApplicationsConnectionString, configuration.GetConnectionString("Applications")},
            {ConfigurationKeys.LoansConnectionString, configuration.GetConnectionString("Loans")},
        };

        return webApplicationFactory.UseSettings(connectionStrings);
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
