namespace Aspekt.UnitTests.Contracts.PrepareContract.BusinessRules;

using Aspekt.Contacts.CreateContact.BusinessRules;
using Aspekt.Common.BusinessRulesEngine;

public sealed class ContactCanBeCreatedOnlyForAdultRuleTests
{
    [Fact]
    internal void Given_customer_age_which_is_less_than_18_Then_validation_should_throw()
    {
        // Arrange

        // Act
        var act = () => BusinessRuleValidator.Validate(new ContactCanBeCreatedOnlyForAdultRule(17));

        // Assert
        act.Should().Throw<BusinessRuleValidationException>().WithMessage("Contact can not be prepared for a person who is not adult");
    }

    [Fact]
    internal void Given_customer_age_which_is_equal_to_18_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var act = () => BusinessRuleValidator.Validate(new ContactCanBeCreatedOnlyForAdultRule(18));

        // Assert
        act.Should().NotThrow<BusinessRuleValidationException>();
    }

    [Fact]
    internal void Given_customer_age_which_is_greater_than_18_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var act = () => BusinessRuleValidator.Validate(new ContactCanBeCreatedOnlyForAdultRule(19));

        // Assert
        act.Should().NotThrow<BusinessRuleValidationException>();
    }
}
