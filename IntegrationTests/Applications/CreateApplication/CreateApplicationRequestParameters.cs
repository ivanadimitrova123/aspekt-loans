namespace Aspekt.IntegrationTests.Applications.CreateApplication;

internal sealed record CreateApplicationRequestParameters(int MinAmount)
{
    private const int MinimumAmount = 100;

    internal static CreateApplicationRequestParameters GetValid() =>
        new(MinimumAmount);

    internal static CreateApplicationRequestParameters GetWithInvalidAmount() =>
        new(MinimumAmount -1);
}
