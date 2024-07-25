namespace Aspekt.Loans.RegisterLoan;

using Aspekt.Common.Events.EventBus;
using Aspekt.Common.Events;
using Applications.ApproveApplication.Events;
using Data;
using Data.Database;
using Events;

internal sealed class ApplicationApprocedEventHandler(
    LoansPersistence persistence,
    IEventBus eventBus) : IIntegrationEventHandler<ApplicationApprovedEvent>
{
    public async Task Handle(ApplicationApprovedEvent @event, CancellationToken cancellationToken)
    {
        var loan = Loan.Register(@event.ApplicationContactId);
        await persistence.Loans.AddAsync(loan, cancellationToken);
        await persistence.SaveChangesAsync(cancellationToken);

        var loanRegisteredEvent = LoanRegisteredEvent.Create(loan.Id);
        await eventBus.PublishAsync(loanRegisteredEvent, cancellationToken);
    }
}
