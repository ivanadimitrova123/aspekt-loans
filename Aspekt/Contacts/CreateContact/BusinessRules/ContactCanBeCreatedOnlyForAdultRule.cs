namespace Aspekt.Contacts.CreateContact.BusinessRules;

using Aspekt.Common.BusinessRulesEngine;

public sealed class ContactCanBeCreatedOnlyForAdultRule : IBusinessRule
{
    private readonly int _age;

    public ContactCanBeCreatedOnlyForAdultRule(int age) => _age = age;

    public bool IsMet() => _age >= 18;

    public string Error => "Contact can not be prepared for a person who is not adult";
}
