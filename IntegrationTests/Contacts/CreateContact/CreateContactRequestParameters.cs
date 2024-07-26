namespace Aspekt.IntegrationTests.Contrats.CreateContact;

internal sealed record CreateContactRequestParameters(int MinAge, int MaxAge)
{
    private const int MinimumAge = 18;
    private const int MaximumAge = 100;

    internal static CreateContactRequestParameters GetValid() =>
        new(MinimumAge, MaximumAge);

    internal static CreateContactRequestParameters GetWithInvalidAge() =>
        new(0, MinimumAge - 1);
}
