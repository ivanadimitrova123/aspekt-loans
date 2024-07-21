namespace Aspekt.Contacts.CreateContact;

public sealed record CreateContactRequest(string Name, string Surname, int Age, string PhoneNumber, string SocialSecurityNumber, string BankAccountNumber); 
