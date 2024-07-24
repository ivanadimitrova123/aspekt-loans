namespace Aspekt.Applications.CreateApplication;

using Common.Validation.Requests;
using Data;
using Data.Database;
using FluentValidation;

internal static class CreateApplicationEndpoint
{
    internal static void MapCreateApplication(this IEndpointRouteBuilder app) => app.MapPost(ApplicationsApiPaths.Create,
            async (CreateApplicationRequest request, IValidator<CreateApplicationRequest> validator,
                ApplicationsPersistence persistence,
                CancellationToken cancellationToken) =>
            {
                var application = Application.Create(request.CountactId, request.Amount, request.PreparedAt);
                await persistence.Applications.AddAsync(application, cancellationToken);
                await persistence.SaveChangesAsync(cancellationToken);

                return Results.Created($"/{ApplicationsApiPaths.Create}/{application.Id}", application.Id);
            })
        .ValidateRequest<CreateApplicationRequest>()
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Triggers creation of a new application",
            Description =
                "This endpoint is used to create a new application.",
        })
        .Produces<string>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status409Conflict)
        .Produces(StatusCodes.Status500InternalServerError);
}
