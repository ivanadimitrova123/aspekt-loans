namespace Aspekt.Applications.ApproveApplication;

using Data.Database;
using Events;
using Common.Validation.Requests;
using Aspekt.Common.Events.EventBus;
using System;

internal static class ApproveApplicationEndpoint
{
    internal static void MapApproveApplication(this IEndpointRouteBuilder app) => app.MapPatch(ApplicationsApiPaths.Approve,
            async (Guid id, ApproveApplicationRequest request,
                ApplicationsPersistence persistence,
                IEventBus bus,
                TimeProvider timeProvider,
                CancellationToken cancellationToken) =>
            {
                var application =
                    await persistence.Applications.FindAsync([id], cancellationToken: cancellationToken);

                if (application is null)
                {
                    return Results.NotFound();
                }

                //var dateNow = timeProvider.GetUtcNow();
                application.Approve(request.ApprovedAt);
                await persistence.SaveChangesAsync(cancellationToken);

                var @event = ApplicationApprovedEvent.Create(
                    application.Id,
                    application.ContactId,
                    application.ApprovedAt!.Value,
                    timeProvider.GetUtcNow());
                await bus.PublishAsync(@event, cancellationToken);

                return Results.NoContent();
            })
        .ValidateRequest<ApproveApplicationRequest>()
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Approves created application",
            Description = "This endpoint is used to approve created application."
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status409Conflict)
        .Produces(StatusCodes.Status500InternalServerError);
}
