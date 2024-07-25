namespace Aspekt.Loans;

using Aspekt.Loans.Data.Database;

internal static class LoansModule
{
    internal static IServiceCollection AddLoans(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);

        return services;
    }

    internal static IApplicationBuilder UseLoans(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseDatabase();

        return applicationBuilder;
    }
}

