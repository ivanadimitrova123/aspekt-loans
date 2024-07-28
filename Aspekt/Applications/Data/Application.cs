using Aspekt.Applications.ApproveApplication.BusinessRules;
using Aspekt.Applications.CreateApplication.BusinessRules;
using Aspekt.Common.BusinessRulesEngine;

namespace Aspekt.Applications.Data
{
    public sealed class Application
    {
        public Guid Id { get; init; }

        public Guid ContactId { get; private set; }
        public int Amount { get; private set; }

        public DateTimeOffset PreparedAt { get; private set; }
        public DateTimeOffset? ApprovedAt { get; private set; }
        public bool Approved => ApprovedAt.HasValue;

        private Application(Guid id, Guid contactId, int amount, DateTimeOffset preparedAt) 
        { 
            Id = id;
            ContactId = contactId;
            Amount = amount;
            PreparedAt = preparedAt;
        }

        internal static Application Create(Guid contactId, int amount, DateTimeOffset preparedAt) 
        {
            BusinessRuleValidator.Validate(new ApplicationCanBeCreatesOnlyForAmountBiggerThen100Rule(amount));
            return new(Guid.NewGuid(),
            contactId,
            amount,
            preparedAt
            );
        }
        internal void Approve(DateTimeOffset approvedAt) 
        {
            BusinessRuleValidator.Validate(new ApplicationCanOnlyBeApprovedWithin30DaysFromCreation(PreparedAt, approvedAt));
            ApprovedAt = approvedAt;
        }
    }
}
