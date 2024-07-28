namespace Aspekt.Loans;

using Aspekt.Loans.PaidOffLoan;
using GetAllLoans;

internal static class LoansEndpoints
{
    internal static void MapLoans(this IEndpointRouteBuilder app)
    {
        app.MapGetAllLoans();
        app.MapPaidOffLoan();
    }
}

