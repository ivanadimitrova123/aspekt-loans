namespace Aspekt.Applications
{
    internal static class ApplicationsApiPaths
    {
        private const string ApplicationsRootApi = $"{ApiPaths.Root}/applications";

        internal const string Create = ApplicationsRootApi;

        internal const string Approve = $"{ApplicationsRootApi}/{{id}}";
    }
}
