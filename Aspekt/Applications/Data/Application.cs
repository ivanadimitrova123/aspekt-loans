namespace Aspekt.Applications.Data
{
    internal sealed class Application
    {
        public Guid Id { get; init; }

        public Guid ContactId { get; init; }

        public DateTimeOffset PreparedAt { get; init; }
        public DateTimeOffset? ApprovedAt { get; private set; }
        public bool Approved => ApprovedAt.HasValue;

        private Application(Guid id, Guid contactId, DateTimeOffset preparedAt) 
        { 
            Id = id;
            ContactId = contactId;
            PreparedAt = preparedAt;
        }

        internal static Application Create(Guid contactId, DateTimeOffset preparedAt) 
        {
            //biznis rule
            return new(Guid.NewGuid(),
            contactId,
            preparedAt
            );
        }
        internal void Approve(DateTimeOffset approvedAt) 
        {
            //biznis rule
            ApprovedAt = approvedAt;
        }
    }
}
