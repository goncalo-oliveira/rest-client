using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Faactory.RestClient
{
    /// <summary>
    /// Basic Authentication extensions
    /// </summary>
    public static class RestClientBasicAuthExtensions
    {
        /// <summary>
        /// Adds basic authentication to the default request headers
        /// </summary>
        /// <param name="username">The basic authentication username</param>
        /// <param name="password">The basic authentication password</param>
        /// <returns>The System.Net.Http.HttpClient instance</returns>
        public static HttpClient AddBasicAuthentication( this HttpClient httpClient, string username, string password )
        {
            httpClient.DefaultRequestHeaders.Authorization = GetHeaderValue( username, password );

            return ( httpClient );
        }

        /// <summary>
        /// Adds basic authentication to the request being configured
        /// </summary>
        /// <param name="username">The basic authentication username</param>
        /// <param name="password">The basic authentication password</param>
        public static void AddBasicAuthentication( this RestRequestOptions options, string username, string password )
            => options.Headers.Authorization = GetHeaderValue( username, password );

        private static AuthenticationHeaderValue GetHeaderValue( string username, string password )
        {
            var authValue = Convert.ToBase64String(
                Encoding.UTF8.GetBytes( string.Concat( username, ":", password ) )
                );

            return ( new AuthenticationHeaderValue( "Basic", authValue ) );
        }
    }
}
