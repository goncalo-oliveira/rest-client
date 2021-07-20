using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Faactory.RestClient
{
    public sealed class RestClient
    {
        public RestClient( HttpClient baseHttpClient )
        {
            HttpClient = baseHttpClient;
        }

        public RestClient( HttpClient baseHttpClient, string baseUrl )
            : this( baseHttpClient )
        {
            HttpClient.BaseAddress = new Uri( baseUrl.TrimEnd( '/' ) );
        }

        public HttpClient HttpClient { get; }

        public Task<RestResponse> GetAsync( string url, CancellationToken cancellationToken = default )
            => SendASync( null, HttpMethod.Get, url, null, cancellationToken );

        public Task<RestResponse> PostAsync( string url, HttpContent content, CancellationToken cancellationToken = default )
            => SendASync( null, HttpMethod.Post, url, content, cancellationToken );

        public Task<RestResponse> PutAsync( string url, HttpContent content, CancellationToken cancellationToken = default )
            => SendASync( null, HttpMethod.Put, url, content, cancellationToken );

        public Task<RestResponse> PatchAsync( string url, HttpContent content, CancellationToken cancellationToken = default )
            => SendASync( null, HttpMethod.Patch, url, content, cancellationToken );

        public Task<RestResponse> DeleteAsync( string url, CancellationToken cancellationToken = default )
            => SendASync( null, HttpMethod.Delete, url, null, cancellationToken );

        internal async Task<RestResponse> SendASync( Action<HttpRequestMessage> configure, HttpMethod method, string url, HttpContent content = null, CancellationToken cancellationToken = default )
        {
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
