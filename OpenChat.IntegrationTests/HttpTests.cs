using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using OpenChat.Api;
using Xunit;

namespace OpenChat.IntegrationTests
{
    // Integration tests definition
    // https://github.com/sandromancuso/cleancoders_openchat/blob/master/APIs.md

    [Collection("Integration Tests")]
    public class HttpTests
    {
        private readonly HttpClient client;

        public HttpTests(WebApplicationFactory<Startup> factory)
        {
            client = factory.CreateClient();
        }

        [Fact]
        public async Task GetRoot_ReturnsSuccessAndStatusUp()
        {
            // Act
            var response = await client.GetAsync("/openchat/");

            // Assert
            response.EnsureSuccessStatusCode();
            dynamic result = await GetContentFromAsync(response);
            Assert.Equal("Up", (string)result.status);
        }

        private static HttpContent GetJsonFrom(object content)
        {
            return new StringContent(
                JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        }

        private static async Task<dynamic> GetContentFromAsync(HttpResponseMessage httpResponse)
        {
            return JsonConvert.DeserializeObject<dynamic>(
                await httpResponse.Content.ReadAsStringAsync());
        }
    }
}
