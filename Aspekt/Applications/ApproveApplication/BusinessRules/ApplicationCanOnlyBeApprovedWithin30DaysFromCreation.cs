namespace Aspekt.Applications.ApproveApplication.BusinessRules;

using Common.BusinessRulesEngine;

public sealed class ApplicationCanOnlyBeApprovedWithin30DaysFromCreation : IBusinessRule
{
    private readonly DateTimeOffset _preparedAt;
    private readonly DateTimeOffset _approvedAt;

    public ApplicationCanOnlyBeApprovedWithin30DaysFromCreation(DateTimeOffset preparedAt,
        DateTimeOffset approvedAt)
    {
        _preparedAt = preparedAt;
        _approvedAt = approvedAt;
    }

    public bool IsMet()
    {
        var timeDifference = _approvedAt.Date - _preparedAt.Date;

        return timeDifference <= TimeSpan.FromDays(30);
    }

    public string Error =>
        "Application can not be approved because more than 30 days have passed from the application creation";
}

