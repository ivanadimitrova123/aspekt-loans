namespace Aspekt.Common.BusinessRulesEngine;

public class BusinessRuleValidationException : InvalidOperationException
{
    public BusinessRuleValidationException(string message) : base(message)
    {
    }
}