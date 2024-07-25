namespace Aspekt.Loans;

using GetAllLoans;

internal static class LoansEndpoints
{
    internal static void MapLoans(this IEndpointRouteBuilder app)
    {
        app.MapGetAllLoans();
    }
}

