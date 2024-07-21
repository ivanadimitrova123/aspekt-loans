namespace Aspekt.Contacts;

using Aspekt.Contacts.CreateContact;

internal static class ContactsEndpoints
{
    internal static void MapContacts(this IEndpointRouteBuilder app)
    {
        app.MapCreateContact();
    }
}
