namespace Aspekt.IntegrationTests.Applications.CreateApplication;

using Aspekt.Applications;
using Common.TestEngine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Aspekt.Applications.Data.Database;

public sealed class CreateApplicationTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private readonly HttpClient _applicationHttpClient;

    public CreateApplicationTests(WebApplicationFactory<Program> applicationFactory, DatabaseContainer database)
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
    }

    [Fact]
    public async Task Given_valid_application_preparation_request_Then_should_return_created_status_code()
    {
        // Arrange
        var requestParameters = CreateApplicationRequestParameters.GetValid();

        // Act
        var prepareApplicationRequest = new CreateApplicationRequestFaker(requestParameters.MinAmount).Generate();

        // Act
        var prepareApplicationResponse = await _applicationHttpClient.PostAsJsonAsync(ApplicationsApiPaths.Create, prepareApplicationRequest);
        var responseContent = await prepareApplicationResponse.Content.ReadAsStringAsync();
        Console.WriteLine($"Response: {responseContent}");

        // Assert
        prepareApplicationResponse.Should().HaveStatusCode(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Given_application_preparation_request_with_invalid_amount_Then_should_return_conflict_status_code()
    {
        // Arrange
        var requestParameters = CreateApplicationRequestParameters.GetWithInvalidAmount();

        var prepareApplicationRequest = new CreateApplicationRequestFaker(requestParameters.MinAmount).Generate();

        // Act
        var prepareApplicationResponse = await _applicationHttpClient.PostAsJsonAsync(ApplicationsApiPaths.Create, prepareApplicationRequest);

        // Assert
        prepareApplicationResponse.Should().HaveStatusCode(HttpStatusCode.Conflict);

        var responseMessage = await prepareApplicationResponse.Content.ReadFromJsonAsync<ProblemDetails>();
        responseMessage?.Status.Should().Be((int)HttpStatusCode.Conflict);
        responseMessage?.Title.Should().Be("Application amount must be bigger then 100");
    }
}
