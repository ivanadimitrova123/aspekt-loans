namespace Aspekt.UnitTests.Contracts.SignContract.RequestValidator;

using Aspekt.Applications.ApproveApplication;
using FluentValidation.TestHelper;

public sealed class ApproveApplicationRequestValidatorTests
{
    private readonly ApproveApplicationRequestValidator _validator = new();
    private readonly DateTimeOffset _fakeNow = new Faker().Date.RecentOffset();

    [Fact]
    internal void Given_approve_application_request_validation_When_request_is_valid_Then_result_should_have_no_errors()
    {
        // Arrange
        var request = new ApproveApplicationRequest(_fakeNow);

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    internal void Given_sign_application_request_validation_When_approved_at_not_provided_Then_result_should_have_error()
    {
        // Arrange
        var request = new ApproveApplicationRequest(default);

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(approveApplicationRequest => approveApplicationRequest.ApprovedAt);
    }
}
