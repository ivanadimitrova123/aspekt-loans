namespace Aspekt.UnitTests.Loans.RequestValidator;

using Aspekt.Loans.PaidOffLoan;
using FluentValidation.TestHelper;

public sealed class PaidOFffLoanRequestValidatorTests
{
    private readonly PaidOffLoanRequestValidator _validator = new();
    private readonly DateTime _fakeNow = new Faker().Date.Recent();

    [Fact]
    internal void Given_payOff_loan_request_validation_When_request_is_valid_Then_result_should_have_no_errors()
    {
        // Arrange
        var request = new PaidOffLoanRequest(_fakeNow);
        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    internal void Given_payOff_loan_request_validation_When_property_is_valid_Then_result_should_have_error()
    {
        var request = new PaidOffLoanRequest(default);

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(request => request.PaidOffAt);
    }
}
