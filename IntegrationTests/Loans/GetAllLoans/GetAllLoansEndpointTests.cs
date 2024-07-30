using Aspekt.Loans.GetAllLoans;
using Aspekt.Loans.Data;
using Newtonsoft.Json;
using FluentAssertions;
using System.Collections.ObjectModel;
namespace Aspekt.IntegrationTests.Loans
{
    public class GetAllLoansEndpointTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public GetAllLoansEndpointTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllLoans_ReturnsSuccessResponse()
        {
            // Arrange
            var requestUrl = "/api/loans"; // Update this with your actual endpoint URL
            var expectedStatusCode = System.Net.HttpStatusCode.OK;

            // Act
            var response = await _client.GetAsync(requestUrl);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(expectedStatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var getAllLoansResponse = JsonConvert.DeserializeObject<GetAllLoansResponse>(responseBody);

            // Check that the response is not null and contains loans
            getAllLoansResponse.Should().NotBeNull();
            getAllLoansResponse.Loans.Should().BeOfType<ReadOnlyCollection<LoanDto>>();
            getAllLoansResponse.Loans.Should().NotBeEmpty();
        }
    }
}