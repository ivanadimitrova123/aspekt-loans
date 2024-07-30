namespace Aspekt.IntegrationTests.Applications.ApproveApplication;

using Aspekt.Applications;
using Common.TestEngine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Aspekt.Applications.Data.Database;
using System.Linq;

public sealed class ApproveApplicationTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private readonly HttpClient _applicationHttpClient;
    private readonly ApplicationsPersistence _applicationsPersistence;

    public ApproveApplicationTests(WebApplicationFactory<Program> applicationFactory, DatabaseContainer database)
    {
        _applicationHttpClient = applicationFactory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<ApplicationsPersistence>(options =>
                        options.UseNpgsql(database.ConnectionString));
                });
            })
            .CreateClient();

        var serviceProvider = applicationFactory.Services.CreateScope().ServiceProvider;
        _applicationsPersistence = serviceProvider.GetRequiredService<ApplicationsPersistence>();
    }

    private async Task<Guid> GetPendingApplicationIdAsync()
    {
        var application = await _applicationsPersistence.Applications
            .Where(a => a.ApprovedAt == null)
            .OrderBy(a => a.PreparedAt) 
            .FirstOrDefaultAsync();
        if (application == null)
        {
            throw new InvalidOperationException("No pending application found in the database.");
        }

        return application.Id; //?? Guid.Empty;
    }

    private async Task<DateTimeOffset> GetApplicationCreationDateAsync(Guid applicationId)
    {
        var application = await _applicationsPersistence.Applications.FindAsync(applicationId);
        return application?.PreparedAt ?? DateTimeOffset.UtcNow;
    }

    [Fact]
    public async Task Given_valid_application_approval_request_Then_should_return_created_status_code()
    {
        // Arrange
        var applicationId = await GetPendingApplicationIdAsync();
        if (applicationId == Guid.Empty)
        {
            Assert.Fail("No pending application found in the database.");
            return;
        }

        var creationDate = await GetApplicationCreationDateAsync(applicationId);
        var requestParameters = ApproveApplicationRequestParameters.GetValid(creationDate);

        var approveApplicationRequest = new ApproveApplicationRequestFaker(requestParameters.DateOfApproval).Generate();
        var basePath = "/api/applications";
        var requestUri = $"{basePath}/{applicationId}";

        // Act
        var prepareApplicationResponse = await _applicationHttpClient.PatchAsJsonAsync(requestUri, approveApplicationRequest);

        // Assert
        prepareApplicationResponse.Should().HaveStatusCode(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Given_application_approval_request_with_invalid_date_Then_should_return_conflict_status_code()
    {
        // Arrange
        var applicationId = await GetPendingApplicationIdAsync();
        if (applicationId == Guid.Empty)
        {
            Assert.Fail("No pending application found in the database.");
            return;
        }

        var creationDate = await GetApplicationCreationDateAsync(applicationId);
        var requestParameters = ApproveApplicationRequestParameters.GetWithInvalidDate(creationDate);

        var approveApplicationRequest = new ApproveApplicationRequestFaker(requestParameters.DateOfApproval).Generate();
        var basePath = "/api/applications";
        var requestUri = $"{basePath}/{applicationId}";
        // Act  
        var prepareApplicationResponse = await _applicationHttpClient.PatchAsJsonAsync(requestUri, approveApplicationRequest);

        // Assert
        prepareApplicationResponse.Should().HaveStatusCode(HttpStatusCode.Conflict);

        var responseMessage = await prepareApplicationResponse.Content.ReadFromJsonAsync<ProblemDetails>();
        responseMessage?.Status.Should().Be((int)HttpStatusCode.Conflict);
        responseMessage?.Title.Should().Be("Application can not be approved because more than 30 days have passed from the application creation");
    }
}
