namespace Aspekt.Applications.ApproveApplication.Events;

using Aspekt.Common.Events;

internal record ApplicationApprovedEvent(
    Guid Id,
    Guid ApplicationId,
    Guid ApplicationContactId,
    DateTimeOffset ApprovedAt,
    DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    internal static ApplicationApprovedEvent Create(
        Guid applicationId,
        Guid applicationContactId,
        DateTimeOffset approvedAt,
        DateTimeOffset occurredAt) =>
        new(Guid.NewGuid(), applicationId, applicationContactId, approvedAt, occurredAt);
}
