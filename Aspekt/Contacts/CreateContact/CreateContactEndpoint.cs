namespace Aspekt.Contacts.CreateContact;

using Aspekt;
using Aspekt.Common.Validation.Requests;
using Aspekt.Contacts.Data.Database;
using FluentValidation;

internal static class CreateContactEndpoint
{
    internal static void MapCreateContact(this IEndpointRouteBuilder app) => app.MapPost(ContactsApiPaths.Create,
        async (CreateContactRequest request, IValidator<CreateContactRequest> validator,
            ContactsPersistence persistence,
            CancellationToken cancellationToken) =>
        {
            var contact = Contact.Create(request.Name, request.Surname, request.Age, request.PhoneNumber, request.SocialSecurityNumber, request.BankAccountNumber);
            await persistence.Contacts.AddAsync(contact, cancellationToken);
            await persistence.SaveChangesAsync(cancellationToken);

            return Results.Created($"/{ContactsApiPaths.Create}/{contact.Id}", contact.Id);
        })
    .ValidateRequest<CreateContactRequest>()
    .WithOpenApi(operation => new(operation)
    {
        Summary = "Triggers creation of a new contact for new customer",
        Description =
            "This endpoint is used to create a new contact for new customers.",
    })
    .Produces<string>(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status409Conflict)
    .Produces(StatusCodes.Status500InternalServerError);
}
