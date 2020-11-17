using System;
using System.Net;
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

        // Register New User
        // POST - openchat/registration { "username" : "Alice", "password" : "alki324d", "about" : "I love playing the piano and travelling." }
        // Success Status CREATED - 201 Response: { "userId" : "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx" "username" : "Alice", "about" : "I love playing the piano and travelling." }
        // Failure Status: BAD_REQUEST - 400 Response: "Username already in use."
        [Fact]
        public async Task Registration_NewUserRegistrationSucceeds()
        {
            // Arrange
            var alice = new { username = "Alice", password = "irrelevant", about = "irrelevant" };

            // Act
            var response = await client.PostAsync("/openchat/registration", GetJsonFrom(alice));

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            dynamic user = await GetContentFromAsync(response);
            Assert.NotEqual(Guid.Empty, (Guid)user.userId);
            Assert.Equal(alice.username, (string)user.username);
        }

        private static HttpContent GetJsonFrom(object content)
        {
            return new StringContent(
                JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        }

        private static async Task<dynamic> GetContentFromAsync(HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<dynamic>(
                await response.Content.ReadAsStringAsync());
        }
    }
}
