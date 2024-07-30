using Aspekt.Common.BusinessRulesEngine;
using Aspekt.Contacts.CreateContact.BusinessRules;
using FluentValidation;

namespace Aspekt.Contacts;

public sealed class Contact
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
    public int Age { get; init; }
    public string PhoneNumber { get; init; }
    public string SocialSecurityNumber { get; init; }
    public string BankAccountNumber { get; init; }

    private Contact(string name, string surname ,int age, string phoneNumber, string socialSecurityNumber, string bankAccountNumber) { 
        Name = name;
        Surname = surname;
        Age = age;
        PhoneNumber = phoneNumber;
        SocialSecurityNumber = socialSecurityNumber;
        BankAccountNumber = bankAccountNumber;
    }

    internal static Contact Create(string name, string surname, int age, string phoneNumber, string socialSecurityNumber, string bankAccountNumber) { 
        
        BusinessRuleValidator.Validate(new ContactCanBeCreatedOnlyForAdultRule(age));

        return new(
            name,
            surname,
            age,
            phoneNumber,
            socialSecurityNumber,
            bankAccountNumber
            );
    }

}

