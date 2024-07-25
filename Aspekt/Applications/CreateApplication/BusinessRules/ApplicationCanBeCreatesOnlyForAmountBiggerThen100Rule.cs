namespace Aspekt.Applications.CreateApplication.BusinessRules;

using Common.BusinessRulesEngine;

public sealed class ApplicationCanBeCreatesOnlyForAmountBiggerThen100Rule : IBusinessRule
{
    private const int MinimumAmount = 100;

    private readonly int _amount;

    public ApplicationCanBeCreatesOnlyForAmountBiggerThen100Rule(int amount) => _amount = amount;

    public bool IsMet() => _amount >= MinimumAmount;

    public string Error => "Application amount must be bigger then 100";
}

