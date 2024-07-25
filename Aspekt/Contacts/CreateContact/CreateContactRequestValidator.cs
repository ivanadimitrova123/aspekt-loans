namespace Aspekt.Contacts.CreateContact;

using FluentValidation;

public sealed class CreateContactRequestValidator : AbstractValidator<CreateContactRequest>
{
    public CreateContactRequestValidator()
    {
        RuleFor(request => request.Age).GreaterThan(0);
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Surname).NotEmpty();
        RuleFor(request => request.PhoneNumber).NotEmpty();
        RuleFor(request => request.SocialSecurityNumber).NotEmpty();
        RuleFor(request => request.BankAccountNumber).NotEmpty();
    }
}