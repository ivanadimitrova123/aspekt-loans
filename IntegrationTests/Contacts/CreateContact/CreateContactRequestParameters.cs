namespace Aspekt.IntegrationTests.Contacts.CreateContact;

internal sealed record RegisterLoanRequestParameters(int MinAge, int MaxAge)
{
    private const int MinimumAge = 18;
    private const int MaximumAge = 100;

    internal static RegisterLoanRequestParameters GetValid() =>
        new(MinimumAge, MaximumAge);

    internal static RegisterLoanRequestParameters GetWithInvalidAge() =>
        new(0, MinimumAge - 1);
}
