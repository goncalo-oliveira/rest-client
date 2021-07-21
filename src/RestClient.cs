using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Faactory.RestClient
{
    /// <summary>
    /// A RestClient instance
    /// </summary>
    public sealed class RestClient
    {
        public RestClient( HttpClient baseHttpClient, Json.IJsonSerializer jsonSerializer = null )
        {
            HttpClient = baseHttpClient;
            JsonSerializer = jsonSerializer ?? new Json.DefaultJsonSerializer();
        }

        public RestClient( HttpClient baseHttpClient, string baseUrl, Json.IJsonSerializer jsonSerializer = null )
            : this( baseHttpClient, jsonSerializer )
        {
            HttpClient.BaseAddress = new Uri( baseUrl.TrimEnd( '/' ) );
        }

        /// <summary>
        /// The underlying HttpClient instance
        /// </summary>
        public HttpClient HttpClient { get; }

        internal Json.IJsonSerializer JsonSerializer { get; }

        /// <summary>
        /// Sends a GET request to the specified resource location
        /// </summary>
        /// <param name="url">The resource location</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        public Task<RestResponse> GetAsync( string url, CancellationToken cancellationToken = default )
            => SendASync( null, HttpMethod.Get, url, null, cancellationToken );

        /// <summary>
        /// Sends a POST request to the specified resource location
        /// </summary>
        /// <param name="url">The resource location</param>
        /// <param name="content">The request body content</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        public Task<RestResponse> PostAsync( string url, HttpContent content, CancellationToken cancellationToken = default )
            => SendASync( null, HttpMethod.Post, url, content, cancellationToken );

        /// <summary>
        /// Sends a PUT request to the specified resource location
        /// </summary>
        /// <param name="url">The resource location</param>
        /// <param name="content">The request body content</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        public Task<RestResponse> PutAsync( string url, HttpContent content, CancellationToken cancellationToken = default )
            => SendASync( null, HttpMethod.Put, url, content, cancellationToken );

        /// <summary>
        /// Sends a PATCH request to the specified resource location
        /// </summary>
        /// <param name="url">The resource location</param>
        /// <param name="content">The request body content</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        public Task<RestResponse> PatchAsync( string url, HttpContent content, CancellationToken cancellationToken = default )
            => SendASync( null, HttpMethod.Patch, url, content, cancellationToken );

        /// <summary>
        /// Sends a DELETE request to the specified resource location
        /// </summary>
        /// <param name="url">The resource location</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        public Task<RestResponse> DeleteAsync( string url, CancellationToken cancellationToken = default )
            => SendASync( null, HttpMethod.Delete, url, null, cancellationToken );

        internal async Task<RestResponse> SendASync( Action<HttpRequestMessage> configure, HttpMethod method, string url, HttpContent content = null, CancellationToken cancellationToken = default )
        {
            if ( url == null )
            {
                throw new ArgumentNullException( nameof( url ), "Request URL cannot be null" );
            }

            var message = new HttpRequestMessage( method, url )
            {
                Content = content
            };

            configure?.Invoke( message );

            var httpResponse = await HttpClient.SendAsync( message, cancellationToken );

            if ( cancellationToken.IsCancellationRequested )
            {
                return ( RestResponse.Empty );
            }

            var restResponse = new RestResponse
            {
                JsonSerializer = JsonSerializer,
                StatusCode = (int)httpResponse.StatusCode,
                Headers = httpResponse.Headers
            };

            try
            {
                restResponse.Content = await httpResponse.Content.ReadAsByteArrayAsync( cancellationToken );
            }
            catch {}

            return ( restResponse );
        }
    }
}
