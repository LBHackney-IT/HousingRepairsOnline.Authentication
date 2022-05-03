using System.Net.Http;
using System.Threading.Tasks;

namespace HousingRepairsOnline.Authentication.Helpers
{
    /// <summary>Helper for authentication token interactions.</summary>
    public static class HttpClientExtensions
    {
        /// <summary>Retrieves an API token.</summary>
        /// <param name="httpClient">A http client with a <see cref="HttpClient.BaseAddress"/> to use for making the token request.</param>
        /// <param name="identifier">The identifier to use for validation.</param>
        /// <returns>An API token.</returns>
        /// <exception cref="System.InvalidOperationException">Exception will be raised if <paramref name="httpClient" /> does not have it's <see cref="HttpClient.BaseAddress"/> set.</exception>
        public static async Task<string> RetrieveAuthenticationToken(this HttpClient httpClient, string identifier)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"authentication?identifier={identifier}");
            var response = await httpClient.SendAsync(request);

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
