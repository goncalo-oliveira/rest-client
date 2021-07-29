using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Faactory.RestClient
{
    /// <summary>
    /// A pre-configured request
    /// </summary>
    public sealed class RestRequest
    {
        private readonly string requestUrl;
        private readonly RestRequestOptions options;

        internal RestRequest( RestClient restClient, RestRequestOptions requestOptions, string url )
        {
            Client = restClient;
            options = requestOptions;
            requestUrl = url;
        }

        /// <summary>
        /// Gets the underlying RestClient instance
        /// </summary>
        public RestClient Client { get; }

        public System.Net.Http.Headers.HttpRequestHeaders Headers => options.Headers;

        /// <summary>
        /// Sends a GET request to the pre-configured resource location
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        public Task<RestResponse> GetAsync( CancellationToken cancellationToken = default )
            => Client.SendASync( Configure, HttpMethod.Get, requestUrl, null, cancellationToken );

        /// <summary>
        /// Sends a GET request to the specified resource location
        /// </summary>
        /// <param name="url">The resource location; overwrites pre-configured location</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        public Task<RestResponse> GetAsync( string url, CancellationToken cancellationToken = default )
            => Client.SendASync( Configure, HttpMethod.Get, url, null, cancellationToken );

        /// <summary>
        /// Sends a POST request to the pre-configured resource location
        /// </summary>
        /// <param name="content">The request body content</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        public Task<RestResponse> PostAsync( HttpContent content, CancellationToken cancellationToken = default )
            => Client.SendASync( Configure, HttpMethod.Post, requestUrl, content, cancellationToken );

        /// <summary>
        /// Sends a POST request to the specified resource location
        /// </summary>
        /// <param name="url">The resource location; overwrites pre-configured location</param>
        /// <param name="content">The request body content</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        public Task<RestResponse> PostAsync( string url, HttpContent content, CancellationToken cancellationToken = default )
            => Client.SendASync( Configure, HttpMethod.Post, url, content, cancellationToken );

        /// <summary>
        /// Sends a PUT request to the pre-configured resource location
        /// </summary>
        /// <param name="content">The request body content</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        public Task<RestResponse> PutAsync( HttpContent content, CancellationToken cancellationToken = default )
            => Client.SendASync( Configure, HttpMethod.Put, requestUrl, content, cancellationToken );

        /// <summary>
        /// Sends a PUT request to the specified resource location
        /// </summary>
        /// <param name="url">The resource location; overwrites pre-configured location</param>
        /// <param name="content">The request body content</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        public Task<RestResponse> PutAsync( string url, HttpContent content, CancellationToken cancellationToken = default )
            => Client.SendASync( Configure, HttpMethod.Put, url, content, cancellationToken );

        /// <summary>
        /// Sends a PATCH request to the pre-configured resource location
        /// </summary>
        /// <param name="content">The request body content</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        public Task<RestResponse> PatchAsync( HttpContent content, CancellationToken cancellationToken = default )
            => Client.SendASync( Configure, HttpMethod.Patch, requestUrl, content, cancellationToken );

        /// <summary>
        /// Sends a PATCH request to the specified resource location
        /// </summary>
        /// <param name="url">The resource location; overwrites pre-configured location</param>
        /// <param name="content">The request body content</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        public Task<RestResponse> PatchAsync( string url, HttpContent content, CancellationToken cancellationToken = default )
            => Client.SendASync( Configure, HttpMethod.Patch, url, content, cancellationToken );

        /// <summary>
        /// Sends a DELETE request to the pre-configured resource location
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        public Task<RestResponse> DeleteAsync( CancellationToken cancellationToken = default )
            => Client.SendASync( Configure, HttpMethod.Delete, requestUrl, null, cancellationToken );

        /// <summary>
        /// Sends a DELETE request to the specified resource location
        /// </summary>
        /// <param name="url">The resource location; overwrites pre-configured location</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        public Task<RestResponse> DeleteAsync( string url, CancellationToken cancellationToken = default )
            => Client.SendASync( Configure, HttpMethod.Delete, url, null, cancellationToken );

        private void Configure( HttpRequestMessage message )
        {
            if ( options.Headers.Any() )
            {
                message.Headers.CopyFrom( options.Headers );
            }

            // include additional query parameters (for specific/override location)
            if ( string.IsNullOrEmpty( requestUrl ) && options.QueryParameters.HasKeys() )
            {
                var resourceUrl = ResourceUrl.FromUri( message.RequestUri );

                // append/overwrite with configured query parameters
                foreach ( var key in options.QueryParameters.AllKeys )
                {
                    resourceUrl.QueryParameters.Set( key, options.QueryParameters[key] );
                }

                message.RequestUri = resourceUrl;
            }
        }
    }
}
