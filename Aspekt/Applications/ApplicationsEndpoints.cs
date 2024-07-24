namespace Aspekt.Applications
{
    internal static class ApplicationsEndpoints
    {
        internal static void MapApplications(this IEndpointRouteBuilder app)
        {
            app.MapCreateApplication();
            app.MapApproveApplication();
        }
}
