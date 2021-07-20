using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Faactory.RestClient
{
    public sealed class RestRequest
    {
        private readonly RestClient client;
        private readonly string requestUrl;
        private readonly RestRequestOptions options;

        internal RestRequest( RestClient restClient, string url, RestRequestOptions requestOptions )
        {
            client = restClient;
            requestUrl = url;
            options = requestOptions;
        }

        public Task<RestResponse> GetAsync( CancellationToken cancellationToken = default )
            => client.SendASync( Configure, HttpMethod.Get, requestUrl, null, cancellationToken );

        public Task<RestResponse> PostAsync( HttpContent content, CancellationToken cancellationToken = default )
            => client.SendASync( Configure, HttpMethod.Post, requestUrl, content, cancellationToken );

        public Task<RestResponse> PutAsync( HttpContent content, CancellationToken cancellationToken = default )
            => client.SendASync( Configure, HttpMethod.Put, requestUrl, content, cancellationToken );

        public Task<RestResponse> PatchAsync( HttpContent content, CancellationToken cancellationToken = default )
            => client.SendASync( Configure, HttpMethod.Patch, requestUrl, content, cancellationToken );

        public Task<RestResponse> DeleteAsync( CancellationToken cancellationToken = default )
            => client.SendASync( Configure, HttpMethod.Delete, requestUrl, null, cancellationToken );

        private void Configure( HttpRequestMessage message )
        {
            if ( options.Headers.Any() )
            {
                message.Headers.CopyFrom( options.Headers );
            }
        }
    }
}
