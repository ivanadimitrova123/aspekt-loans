namespace Aspekt.UnitTests.Contacts.CreateContact;

internal sealed record CreateContactParameters(int MinAge, int MaxAge)
{
    private const int MinimumAge = 18;
    private const int MaximumAge = 100;

    internal static CreateContactParameters GetValid() => new(MinimumAge, MaximumAge);
}
