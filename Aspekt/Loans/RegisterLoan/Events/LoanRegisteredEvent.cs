namespace Aspekt.Loans.RegisterLoan.Events;

using Aspekt.Common.Events;

internal record LoanRegisteredEvent(Guid Id, Guid LoanId, DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    internal static LoanRegisteredEvent Create(Guid loanId) =>
        new(Guid.NewGuid(), loanId, DateTimeOffset.UtcNow);
}
