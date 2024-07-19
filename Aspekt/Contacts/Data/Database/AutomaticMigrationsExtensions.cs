namespace EvolutionaryArchitecture.Fitnet.Contracts.Data.Database;

using Microsoft.EntityFrameworkCore;

internal static class AutomaticMigrationsExtensions
{
    internal static IApplicationBuilder UseAutomaticMigrations(this IApplicationBuilder applicationBuilder)
    {
        //provides access to the application's request processing pipeline.
        //CreateScope(): Creates a new scope for dependency injection (DI) services 
        using var scope = applicationBuilder.ApplicationServices.CreateScope();
        //ContractsPersistence is a class representing the database context
        var context = scope.ServiceProvider.GetRequiredService<ContactsPersistence>();
        //Migrate(): Applies any pending migrations for the database. 
        context.Database.Migrate();

        return applicationBuilder;
    }
}
