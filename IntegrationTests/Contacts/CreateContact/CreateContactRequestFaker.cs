namespace Aspekt.IntegrationTests.Contacts.CreateContact;

using Aspekt.Contacts.CreateContact;

internal sealed class CreateContactRequestFaker : Faker<CreateContactRequest>
{
    internal CreateContactRequestFaker(int minAge, int maxAge) => CustomInstantiator(faker =>
        new CreateContactRequest(
            faker.Random.String(),
            faker.Random.String(),
            faker.Random.Number(minAge, maxAge),
            faker.Random.String(),
            faker.Random.String(),
            faker.Random.String()
        )
    );
}
