namespace Aspekt.UnitTests.Contracts.SignContract.RequestValidator;

using Aspekt.Applications.CreateApplication;
using FluentValidation.TestHelper;

public sealed class CreateApplicationRequestValidatorTests
{
    private readonly CreateApplicationRequestValidator _validator = new();
    private readonly DateTimeOffset _fakeNowDate = new Faker().Date.RecentOffset();
    private readonly Guid _fakeNowID = new Faker().Random.Guid();
    private readonly int _fakeNowAmount = new Faker().Random.Int();

    [Fact]
    internal void Given_create_application_request_validation_When_request_is_valid_Then_result_should_have_no_errors()
    {
        // Arrange
        var request = new CreateApplicationRequest(_fakeNowID, 101, _fakeNowDate);

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    internal void Given_create_application_request_validation_When_approved_at_not_provided_Then_result_should_have_error()
    {
        // Arrange
        var request = new CreateApplicationRequest(default, default, default);

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(createApplicationRequest => createApplicationRequest.CountactId);
    }
}
