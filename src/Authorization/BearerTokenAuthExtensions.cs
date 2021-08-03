using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Faactory.RestClient
{
    /// <summary>
    /// Bearer Token extensions
    /// </summary>
    public static class BearerTokenAuthExtensions
    {
        /// <summary>
        /// Adds a Bearer token to the default request headers
        /// </summary>
        /// <param name="token">The token</param>
        /// <returns>The System.Net.Http.HttpClient instance</returns>
        public static HttpClient AddBearerToken( this HttpClient httpClient, string token )
        {
            httpClient.DefaultRequestHeaders.Authorization = GetHeaderValue( token );

            return ( httpClient );
        }

        /// <summary>
        /// Adds a Bearer token to the request being configured
        /// </summary>
        /// <param name="token">The token</param>
        public static void AddBearerToken( this RestRequestOptions options, string token )
            => options.Headers.Authorization = GetHeaderValue( token );

        private static AuthenticationHeaderValue GetHeaderValue( string token )
            => ( new AuthenticationHeaderValue( "Bearer", token ) );
    }
}
