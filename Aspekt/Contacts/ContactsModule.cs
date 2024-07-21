namespace Aspekt.Contacts;

using Aspekt.Contacts.Data.Database;

internal static class ContactsModule
{
    internal static IServiceCollection AddContacts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);

        return services;
    }

    internal static IApplicationBuilder UseContacts(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseDatabase();

        return applicationBuilder;
    }
}
