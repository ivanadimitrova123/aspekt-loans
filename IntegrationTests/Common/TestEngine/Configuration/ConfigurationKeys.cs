namespace Aspekt.IntegrationTests.Common.TestEngine.Configuration;

internal static class ConfigurationKeys
{
    private const string ConnectionStringsSection = "ConnectionStrings";
    internal const string ContactsConnectionString = $"{ConnectionStringsSection}:Contacts";
    internal const string ApplicationsConnectionString = $"{ConnectionStringsSection}:Applications";
    internal const string LoansConnectionString = $"{ConnectionStringsSection}:Loans";

}