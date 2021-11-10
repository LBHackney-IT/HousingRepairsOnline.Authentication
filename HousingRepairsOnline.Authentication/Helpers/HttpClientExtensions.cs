using System.Net.Http;
using System.Threading.Tasks;

namespace HousingRepairsOnline.Authentication.Helpers
{
    /// <summary>Helper for authentication token interactions.</summary>
    public static class HttpClientExtensions
    {
        /// <summary>Retrieves an API token.</summary>
        /// <param name="httpClient">A http client to use for making the token request.</param>
        /// <param name="apiUrl">The API url to request authentication for.</param>
        /// <param name="identifier">The identifier to use for validation.</param>
        /// <returns>An API token.</returns>
        public static async Task<string> RetrieveAuthenticationToken(this HttpClient httpClient, string apiUrl, string identifier)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{apiUrl}/authentication?identifier={identifier}");
            var response = await httpClient.SendAsync(request);

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
