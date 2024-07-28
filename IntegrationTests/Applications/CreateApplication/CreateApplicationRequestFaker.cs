namespace Aspekt.IntegrationTests.Applications.CreateApplication;

using Aspekt.Applications.CreateApplication;
using Castle.Core.Resource;

internal sealed class CreateApplicationRequestFaker : Faker<CreateApplicationRequest>
{
    internal CreateApplicationRequestFaker(int amount) => CustomInstantiator(faker =>
        new CreateApplicationRequest(
            faker.Random.Guid(),
            amount,
            faker.Date.RecentOffset().ToUniversalTime()

        )
    );
}
