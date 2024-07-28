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
    private async Task<DateTimeOffset> GetApplicationCreationDateAsync(Guid applicationId)
    {
        var application = await _applicationsPersistence.Applications.FindAsync(applicationId);
        return application?.PreparedAt ?? DateTimeOffset.UtcNow;
    }

    [Fact]
    public async Task Given_valid_application_approval_request_Then_should_return_created_status_code()
    {
        // Arrange
        var applicationId = Guid.NewGuid(); // Or fetch a real application ID from your test setup
        var creationDate = await GetApplicationCreationDateAsync(applicationId);
        var requestParameters = ApproveApplicationRequestParameters.GetValid(creationDate);

        // Act
        var approveApplicationRequest = new ApproveApplicationRequestFaker(requestParameters.DateOfApproval).Generate();

        // Act
        var prepareApplicationResponse = await _applicationHttpClient.PostAsJsonAsync(ApplicationsApiPaths.Approve, approveApplicationRequest);
        var responseContent = await prepareApplicationResponse.Content.ReadAsStringAsync();

        // Assert
        prepareApplicationResponse.Should().HaveStatusCode(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Given_application_approval_request_with_invalid_date_Then_should_return_conflict_status_code()
    {
        // Arrange
        var applicationId = Guid.NewGuid();
        var creationDate = await GetApplicationCreationDateAsync(applicationId);
        var requestParameters = ApproveApplicationRequestParameters.GetWithInvalidDate(creationDate);

        var approveApplicationRequest = new ApproveApplicationRequestFaker(requestParameters.DateOfApproval).Generate();

        // Act  
        var prepareApplicationResponse = await _applicationHttpClient.PostAsJsonAsync(ApplicationsApiPaths.Approve, approveApplicationRequest);

        // Assert
        prepareApplicationResponse.Should().HaveStatusCode(HttpStatusCode.Conflict);

        var responseMessage = await prepareApplicationResponse.Content.ReadFromJsonAsync<ProblemDetails>();
        responseMessage?.Status.Should().Be((int)HttpStatusCode.Conflict);
        responseMessage?.Title.Should().Be("Application can not be approved because more than 30 days have passed from the application creation");
    }
}
