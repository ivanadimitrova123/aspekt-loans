namespace Aspekt.Loans;

internal static class LoansApiPaths
{
    internal const string GetAll = $"{ApiPaths.Root}/loans";
    internal const string PaidOff = $"{ApiPaths.Root}/loans/{{id}}";

}