namespace EvolutionaryArchitecture.Aspekt.Offers.Data.Database;

using Microsoft.EntityFrameworkCore;

internal static class AutomaticMigrationsExtensions
{
    internal static IApplicationBuilder UseAutomaticMigrations(this IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationsPersistence>();
        context.Database.Migrate();

        return applicationBuilder;
    }
}