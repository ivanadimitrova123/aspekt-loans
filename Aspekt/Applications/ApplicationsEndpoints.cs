namespace Aspekt.Applications;
using Aspekt.Applications.CreateApplication;

internal static class ApplicationsEndpoints
{
    internal static void MapApplications(this IEndpointRouteBuilder app)
    {
        app.MapCreateApplication();
        //  app.MapApproveApplication();
    }
}
