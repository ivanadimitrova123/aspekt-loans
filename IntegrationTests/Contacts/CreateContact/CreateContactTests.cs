namespace Aspekt.IntegrationTests.Contrats.CreateContact;

using Aspekt.Contacts;
using Aspekt.Contacts.CreateContact;
using Common.TestEngine;
using Common.TestEngine.Configuration;
using Microsoft.AspNetCore.Mvc;

public sealed class CreateContactTests(
    WebApplicationFactory<Program> applicationInMemoryFactory,
    DatabaseContainer database) : IClassFixture<WebApplicationFactory<Program>>,
    IClassFixture<DatabaseContainer>
{
    private readonly HttpClient _applicationHttpClient = applicationInMemoryFactory
        .WithContainerDatabaseConfigured(database.ConnectionString!)
        .CreateClient();

    [Fact]
    internal async Task Given_valid_contract_preparation_request_Then_should_return_created_status_code()
    {
        // Arrange
        var requestParameters = CreateContactRequestParameters.GetValid();

        // Act
        var prepareContractResponse = await PrepareCorrectContract(requestParameters);

        // Assert
        prepareContractResponse.Should().HaveStatusCode(HttpStatusCode.Created);
    }

    [Fact]
    internal async Task Given_contract_preparation_request_with_invalid_age_Then_should_return_conflict_status_code()
    {
        // Arrange
        var requestParameters = CreateContactRequestParameters.GetWithInvalidAge();

        CreateContactRequest prepareContractRequest = new CreateContactRequestFaker(requestParameters.MinAge,
            requestParameters.MaxAge);

        // Act
        var prepareContractResponse =
            await _applicationHttpClient.PostAsJsonAsync(ContactsApiPaths.Create, prepareContractRequest);

        // Assert
        prepareContractResponse.Should().HaveStatusCode(HttpStatusCode.Conflict);

        var responseMessage = await prepareContractResponse.Content.ReadFromJsonAsync<ProblemDetails>();
        responseMessage?.Status.Should().Be((int)HttpStatusCode.Conflict);
        responseMessage?.Title.Should().Be("Contract can not be prepared for a person who is not adult");
    }

    [Fact]
    internal async Task Given_contract_preparation_request_with_invalid_height_Then_should_return_conflict_status_code()
    {
        // Arrange
        var requestParameters = CreateContactRequestParameters.GetWithInvalidHeight();

        CreateContactRequest prepareContractRequest = new CreateContactRequestFaker(requestParameters.MinAge,
            requestParameters.MaxAge, requestParameters.MinHeight, requestParameters.MaxHeight);

        // Act
        var prepareContractResponse =
            await _applicationHttpClient.PostAsJsonAsync(ContactsApiPaths.Create, prepareContractRequest);

        // Assert
        prepareContractResponse.Should().HaveStatusCode(HttpStatusCode.Conflict);

        var responseMessage = await prepareContractResponse.Content.ReadFromJsonAsync<ProblemDetails>();
        responseMessage?.Status.Should().Be((int)HttpStatusCode.Conflict);
        responseMessage?.Title.Should().Be("Customer height must fit maximum limit for gym instruments");
    }

    [Fact]
    internal async Task Given_contract_preparation_request_When_contract_for_customer_was_prepared_earlier_and_was_not_signed_yet_Then_should_return_conflict_status_code()
    {
        // Arrange
        var requestParameters = CreateContactRequestParameters.GetValid();
        var customerId = Guid.NewGuid();
        await PrepareCorrectContract(requestParameters, customerId);

        //Act
        var prepareContractResponse = await PrepareCorrectContract(requestParameters, customerId);

        // Assert
        prepareContractResponse.Should().HaveStatusCode(HttpStatusCode.Conflict);
        var responseMessage = await prepareContractResponse.Content.ReadFromJsonAsync<ProblemDetails>();
        responseMessage?.Status.Should().Be((int)HttpStatusCode.Conflict);
        responseMessage?.Title.Should().Be("Previous contact must be signed by the customer");
    }

    private async Task<HttpResponseMessage> PrepareCorrectContract(CreateContactRequestParameters requestParameters, Guid? customerId = null)
    {
        PrepareContractRequest prepareContractRequest = new CreateContactRequestFaker(requestParameters.MinAge,
            requestParameters.MaxAge, customerId);
        var prepareContractResponse =
            await _applicationHttpClient.PostAsJsonAsync(ContactsApiPaths.Create, prepareContractRequest);
        return prepareContractResponse;
    }
}
