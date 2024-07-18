namespace Aspekt.Contacts;

internal sealed class Contact
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
    public string PhoneNumber { get; init; }

    //Personal identification number
    public string PIN { get; init; }
    public string BankAccountNumber { get; init; }


}

