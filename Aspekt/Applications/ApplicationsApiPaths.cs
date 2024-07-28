namespace Aspekt.Applications
{
    public static class ApplicationsApiPaths
    {
        private const string ApplicationsRootApi = $"{ApiPaths.Root}/applications";

        public const string Create = ApplicationsRootApi;

        public const string Approve = $"{ApplicationsRootApi}/{{id}}";
    }
}
