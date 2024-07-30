namespace Aspekt.UnitTests.Contacts.CreateContact.RequestValidator;

using Aspekt.Contacts.CreateContact;

internal sealed class InvalidCreateContactRequestTestCases : IEnumerable<object[]>
{
    private readonly Faker _faker = new();
    private readonly string _fakeNowStr = new Faker().Random.String2(10);
    public IEnumerator<object[]> GetEnumerator()
    {
        var validContractParameters = CreateContactParameters.GetValid();
        yield return new object[] { new CreateContactRequest(_fakeNowStr, _fakeNowStr, default, _fakeNowStr, _fakeNowStr, _fakeNowStr), nameof(CreateContactRequest.Age) };
        yield return new object[] { new CreateContactRequest(_fakeNowStr, _fakeNowStr, _faker.Random.Number(-100, -1), _fakeNowStr, _fakeNowStr, _fakeNowStr), nameof(CreateContactRequest.Age) };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
