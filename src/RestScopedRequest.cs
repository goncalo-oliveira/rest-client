using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Faactory.RestClient
{
    /// <summary>
    /// A scoped, pre-configured request
    /// </summary>
    public sealed class RestScopedRequest
    {
        private readonly string requestUrl;
        private readonly RestRequestOptions options;

        internal RestScopedRequest( RestClient restClient, string url, RestRequestOptions requestOptions )
        {
            Client = restClient;
            requestUrl = url;
            options = requestOptions;
        }

        /// <summary>
        /// Gets the underlying RestClient instance
        /// </summary>
        public RestClient Client { get; }

        /// <summary>
        /// Sends a GET request to the pre-configured resource location
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        public Task<RestResponse> GetAsync( CancellationToken cancellationToken = default )
            => Client.SendASync( Configure, HttpMethod.Get, requestUrl, null, cancellationToken );

        /// <summary>
        /// Sends a POST request to the pre-configured resource location
        /// </summary>
        /// <param name="content">The request body content</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        public Task<RestResponse> PostAsync( HttpContent content, CancellationToken cancellationToken = default )
            => Client.SendASync( Configure, HttpMethod.Post, requestUrl, content, cancellationToken );

        /// <summary>
        /// Sends a PUT request to the pre-configured resource location
        /// </summary>
        /// <param name="content">The request body content</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        public Task<RestResponse> PutAsync( HttpContent content, CancellationToken cancellationToken = default )
            => Client.SendASync( Configure, HttpMethod.Put, requestUrl, content, cancellationToken );

        /// <summary>
        /// Sends a PATCH request to the pre-configured resource location
        /// </summary>
        /// <param name="content">The request body content</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        public Task<RestResponse> PatchAsync( HttpContent content, CancellationToken cancellationToken = default )
            => Client.SendASync( Configure, HttpMethod.Patch, requestUrl, content, cancellationToken );

        /// <summary>
        /// Sends a DELETE request to the pre-configured resource location
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        public Task<RestResponse> DeleteAsync( CancellationToken cancellationToken = default )
            => Client.SendASync( Configure, HttpMethod.Delete, requestUrl, null, cancellationToken );

        private void Configure( HttpRequestMessage message )
        {
            if ( options.Headers.Any() )
            {
                message.Headers.CopyFrom( options.Headers );
            }
        }
    }
}
