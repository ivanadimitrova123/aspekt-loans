namespace Aspekt.IntegrationTests.Applications.ApproveApplication;

using Aspekt.Applications.ApproveApplication;
using Castle.Core.Resource;

internal sealed class ApproveApplicationRequestFaker : Faker<ApproveApplicationRequest>
{
    internal ApproveApplicationRequestFaker(DateTimeOffset ApprovedAt) => CustomInstantiator(faker =>
        new ApproveApplicationRequest(
            ApprovedAt

        )
    );
}