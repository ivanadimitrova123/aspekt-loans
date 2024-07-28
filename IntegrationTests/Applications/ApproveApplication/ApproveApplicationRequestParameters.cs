namespace Aspekt.IntegrationTests.Applications.ApproveApplication;

internal sealed record ApproveApplicationRequestParameters(DateTimeOffset DateOfApproval)
{
    internal static ApproveApplicationRequestParameters GetValid(DateTimeOffset applicationCreationDate) =>
        new(applicationCreationDate);

    internal static ApproveApplicationRequestParameters GetWithInvalidDate(DateTimeOffset applicationCreationDate) =>
        new(applicationCreationDate.AddDays(31));
}
