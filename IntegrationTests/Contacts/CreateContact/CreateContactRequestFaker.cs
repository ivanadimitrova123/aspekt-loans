namespace Aspekt.IntegrationTests.Contrats.CreateContact;

using Aspekt.Contacts.CreateContact;

internal sealed class PrepareContactRequestFaker : Faker<CreateContactRequest>
{
    internal CreateContactRequestFaker(int minAge, int maxAge, int minHeight, int maxHeight,
        Guid? customerId = null) => CustomInstantiator(faker =>
        new CreateContactRequest(
            customerId ?? faker.Random.Guid(),
            faker.Random.Number(minAge, maxAge),
            faker.Random.Number(minHeight, maxHeight),
            faker.Date.RecentOffset().ToUniversalTime()
        )
    );
}
