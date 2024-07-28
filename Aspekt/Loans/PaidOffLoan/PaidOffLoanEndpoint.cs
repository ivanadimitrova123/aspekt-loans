namespace Aspekt.Loans.PaidOffLoan;

using Data.Database;
using Aspekt.Common.Events.EventBus;
using Common.Validation.Requests;

internal static class PaidOffLoanEndpoint
{
    internal static void MapPaidOffLoan(this IEndpointRouteBuilder app) => app.MapPatch(LoansApiPaths.PaidOff,
            async (Guid id, PaidOffLoanRequest request,
                LoansPersistence persistence,
                IEventBus bus,
                TimeProvider timeProvider,
                CancellationToken cancellationToken) =>
            {
                var loan =
                    await persistence.Loans.FindAsync([id], cancellationToken: cancellationToken);

                if (loan is null)
                {
                    return Results.NotFound();
                }

                loan.PaidOff(request.PaidOffAt);
                await persistence.SaveChangesAsync(cancellationToken);

                return Results.NoContent();
            })
        .ValidateRequest<PaidOffLoanRequest>()
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Pays off loan",
            Description = "This endpoint is used to pay off loan by customer."
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status409Conflict)
        .Produces(StatusCodes.Status500InternalServerError);
}
