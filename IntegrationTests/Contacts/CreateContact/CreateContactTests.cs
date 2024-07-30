namespace Aspekt.IntegrationTests.Contacts.CreateContact;

using Aspekt.Contacts;
using Common.TestEngine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Aspekt.Contacts.Data.Database;

public sealed class CreateApplicationtTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private readonly HttpClient _applicationHttpClient;

    public CreateApplicationtTests(WebApplicationFactory<Program> applicationFactory, DatabaseContainer database)
    {
        _applicationHttpClient = applicationFactory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<ContactsPersistence>(options =>
                        options.UseNpgsql(database.ConnectionString));
                });
            })
            .CreateClient();
    }

    [Fact]
    public async Task Given_valid_contact_preparation_request_Then_should_return_created_status_code()
    {
        // Arrange
        var requestParameters = RegisterLoanRequestParameters.GetValid();

        // Act
        var prepareContactRequest = new CreateContactRequestFaker(requestParameters.MinAge, requestParameters.MaxAge).Generate();

        // Act
        var prepareContactResponse = await _applicationHttpClient.PostAsJsonAsync(ContactsApiPaths.Create, prepareContactRequest);
       // var responseContent = await prepareContactResponse.Content.ReadAsStringAsync();
        // Assert
        prepareContactResponse.Should().HaveStatusCode(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Given_contact_preparation_request_with_invalid_age_Then_should_return_conflict_status_code()
    {
        // Arrange
        var requestParameters = RegisterLoanRequestParameters.GetWithInvalidAge();

        var prepareContactRequest = new CreateContactRequestFaker(requestParameters.MinAge, requestParameters.MaxAge).Generate();

        // Act
        var prepareContactResponse = await _applicationHttpClient.PostAsJsonAsync(ContactsApiPaths.Create, prepareContactRequest);

        // Assert
        prepareContactResponse.Should().HaveStatusCode(HttpStatusCode.Conflict);

        var responseMessage = await prepareContactResponse.Content.ReadFromJsonAsync<ProblemDetails>();
        responseMessage?.Status.Should().Be((int)HttpStatusCode.Conflict);
        responseMessage?.Title.Should().Be("Contact can not be prepared for a person who is not adult");
    }

}
