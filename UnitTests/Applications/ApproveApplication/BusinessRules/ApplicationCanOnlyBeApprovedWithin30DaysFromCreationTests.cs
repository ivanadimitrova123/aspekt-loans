namespace Aspekt.UnitTests.Contracts.SignContract.BusinessRules;

using Aspekt.Applications.ApproveApplication.BusinessRules;
using Aspekt.Common.BusinessRulesEngine;

public sealed class ApplicationCanOnlyBeApprovedWithin30DaysFromCreationTests
{
    [Fact]
    internal void Given_approved_at_date_which_is_more_than_30_days_from_created_at_date_Then_validation_should_throw()
    {
        // Arrange

        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                new ApplicationCanOnlyBeApprovedWithin30DaysFromCreation(DateTimeOffset.Now,
                    DateTimeOffset.Now.AddDays(31)));

        // Assert
        act.Should().Throw<BusinessRuleValidationException>().WithMessage(
            "Application can not be approved because more than 30 days have passed from the application creation");
    }

    [Fact]
    internal void Given_approved_at_date_which_is_30_days_from_created_at_date_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                new ApplicationCanOnlyBeApprovedWithin30DaysFromCreation(DateTimeOffset.Now,
                    DateTimeOffset.Now.AddDays(30)));

        // Assert
        act.Should().NotThrow<BusinessRuleValidationException>();
    }

    [Fact]
    internal void Given_approved_at_date_which_is_less_than_30_days_from_created_at_date_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                new ApplicationCanOnlyBeApprovedWithin30DaysFromCreation(DateTimeOffset.Now,
                    DateTimeOffset.Now.AddDays(29)));

        // Assert
        act.Should().NotThrow<BusinessRuleValidationException>();
    }
}
