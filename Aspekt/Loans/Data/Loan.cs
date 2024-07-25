namespace Aspekt.Loans.Data;
internal sealed class Loan
{
    public Guid Id { get; init; }
    public Guid ContactId { get; init; }

    private Loan(Guid id, Guid contactId)
    {
        Id = id;
        ContactId = contactId;
    }

    internal static Loan Register(Guid contactId) =>
        new(Guid.NewGuid(), contactId);
}

