namespace Aspekt.UnitTests.Contracts.PrepareContract.RequestValidator;

using Aspekt.Contacts.CreateContact;
using Aspekt.UnitTests.Contacts.CreateContact.RequestValidator;
using FluentValidation.TestHelper;

public sealed class CreateContactRequestValidatorTests
{
    private readonly CreateContactRequestValidator _validator = new();
    private readonly string _fakeNowStr = new Faker().Random.String();
    private readonly int _fakeNowInt = new Faker().Random.Int();

    [Fact]
    internal void Given_prepare_contact_request_validation_When_request_is_valid_Then_result_should_have_no_errors()
    {
        // Arrange
        var request = new CreateContactRequest(_fakeNowStr, _fakeNowStr, 66, _fakeNowStr, _fakeNowStr, _fakeNowStr);
        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [ClassData(typeof(InvalidCreateContactRequestTestCases))]
    internal void Given_prepare_contact_request_validation_When_property_is_valid_Then_result_should_have_error(CreateContactRequest request, string expectedInvalidPropertyName)
    {
        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(expectedInvalidPropertyName);
    }
}
