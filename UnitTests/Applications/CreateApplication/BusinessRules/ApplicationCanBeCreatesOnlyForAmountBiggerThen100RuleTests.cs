namespace Aspekt.UnitTests.Contracts.SignContract.BusinessRules;

using Aspekt.Applications.CreateApplication.BusinessRules;
using Aspekt.Common.BusinessRulesEngine;

public sealed class ApplicationCanBeCreatesOnlyForAmountBiggerThen100RuleTests
{
    [Fact]
    internal void Given_amount_which_is_less_than_100_Then_validation_should_throw()
    {
        // Arrange

        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                new ApplicationCanBeCreatesOnlyForAmountBiggerThen100Rule(99));

        // Assert
        act.Should().Throw<BusinessRuleValidationException>().WithMessage(
            "Application amount must be bigger then 100");
    }

    [Fact]
    internal void Given_amount_which_is_equal_to_100_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                  new ApplicationCanBeCreatesOnlyForAmountBiggerThen100Rule(100));


        // Assert
        act.Should().NotThrow<BusinessRuleValidationException>();
    }

    [Fact]
    internal void Given_amount_which_is_more_then_100_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                new ApplicationCanBeCreatesOnlyForAmountBiggerThen100Rule(101));

        // Assert
        act.Should().NotThrow<BusinessRuleValidationException>();
    }
}
