using Aspekt.Contacts.Data.Database;

namespace Aspekt.Applications
{
    internal static class ApplicationsModule
    {
        internal static IServiceCollection AddApplications(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);

            return services;
        }

        internal static IApplicationBuilder UseApplications(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseDatabase();

            return applicationBuilder;
        }
    }
}
