namespace Aspekt.Applications.CreateApplication;

public sealed record CreateApplicationRequest(Guid CountactId, int Amount, DateTimeOffset PreparedAt);
