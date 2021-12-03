using System.Net.Http;
using System.Net.Http.Headers;

namespace HousingRepairsOnline.Authentication.Helpers
{
    /// <summary>Extension methods for <see cref="HttpRequestMessage"/>.</summary>
    public static class HttpRequestMessageExtensions
    {
        /// <summary>Setup a request with an authentication header with a value of a retrieved JWT token.</summary>
        /// <param name="httpRequestMessage">The request message to setup with JWT authentication.</param>
        /// <param name="httpClient">A http client with a <see cref="HttpClient.BaseAddress"/> to use for making the token request.</param>
        /// <param name="authenticationIdentifier">The identifier to use for validation.</param>
        /// <remarks>An HTTP request is made during this method call.</remarks>
        /// <exception cref="System.InvalidOperationException">Exception will be raised if <paramref name="httpClient" /> does not have it's <see cref="HttpClient.BaseAddress"/> set.</exception>
        public static void SetupJwtAuthentication(this HttpRequestMessage httpRequestMessage, HttpClient httpClient, string authenticationIdentifier)
        {
            var retrieveAuthenticationTokenTask = httpClient.RetrieveAuthenticationToken(authenticationIdentifier);
            retrieveAuthenticationTokenTask.Wait();
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", $"{retrieveAuthenticationTokenTask.Result}");
        }
    }
}
