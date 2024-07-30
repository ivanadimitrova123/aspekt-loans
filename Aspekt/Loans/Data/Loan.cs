using Aspekt.Common.BusinessRulesEngine;

namespace Aspekt.Loans.Data;
public sealed class Loan
{
    public Guid Id { get; init; }
    public Guid ContactId { get; init; }

    public DateTimeOffset? PaidOffAt { get; private set; }

    public bool IsPaidOff => PaidOffAt.HasValue;

    private Loan(Guid id, Guid contactId)
    {
        Id = id;
        ContactId = contactId;
    }

    internal static Loan Register(Guid contactId) =>
        new(Guid.NewGuid(), contactId);

    internal void PaidOff(DateTimeOffset paidOffAt)
    {
        PaidOffAt = paidOffAt;
    }
}

