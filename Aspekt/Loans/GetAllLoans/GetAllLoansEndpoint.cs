using Aspekt.Loans.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace Aspekt.Loans.GetAllLoans;

internal static class GetAllLoansEndpoint
{
    internal static void MapGetAllLoans(this IEndpointRouteBuilder app) =>
        app.MapGet(LoansApiPaths.GetAll, async (LoansPersistence persistence, CancellationToken cancellationToken) =>
        {
            var loans = await persistence.Loans
                .AsNoTracking()
                .Select(loans => LoanDto.From(loans))
                .ToListAsync(cancellationToken);
            var response = GetAllLoansResponse.Create(loans);

            return Results.Ok(response);
        })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Returns all loans that exist in the system",
                Description =
                    "This endpoint is used to retrieve all existing loans.",
            })
            .Produces<GetAllLoansResponse>()
            .Produces(StatusCodes.Status500InternalServerError);
}